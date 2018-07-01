namespace Chushka.App.Controllers
{
    using Chushka.Models;
    using Models.BindingModels;
    using SoftUni.WebServer.Common;
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Interfaces;
    using System.Linq;

    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid register details.");
                return this.View();
            }

            var username = model.Username;
            var password = PasswordUtilities.GetPasswordHash(model.Password);
            var fullName = model.FullName;
            var email = model.Email;

            var user = new User
            {
                Username = username,
                Password = password,
                FullName = fullName,
                Email = email
            };

            this.Context.Users.Add(user);
            this.Context.SaveChanges();

            if (this.Context.Users.Count() == 1)
            {
                var admin = this.Context.Users.First();

                var role = new Role
                {
                    UserId = admin.Id,
                    Name = "Admin"
                };

                admin.Role = role;

                this.Context.Roles.Add(role);
                this.Context.SaveChanges();
            }
            else
            {
                var notAdmin = this.Context.Users
                    .FirstOrDefault(u => u.Username == username);

                var role = new Role
                {
                    UserId = notAdmin.Id,
                    Name = "User"
                };

                this.Context.Roles.Add(role);
                this.Context.SaveChanges();
            }

            var currentUser = this.Context
                .Users
                .FirstOrDefault(u => u.Username == username);

            var roles = this.Context
                .Roles
                .Where(r => r.UserId == currentUser.Id)
                .Select(r => r.Name);

            this.SignIn(user.Username, user.Id, roles);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError("All fields required.");
                return this.View();
            }

            var username = model.Username;
            var password = PasswordUtilities.GetPasswordHash(model.Password);

            var user = this.Context
                .Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                this.ShowError("Username or password invalid!");
                return this.View();
            }

            var roles = this.Context
                    .Roles
                    .Where(r => r.UserId == user.Id)
                    .Select(r => r.Name);

            this.SignIn(user.Username, user.Id, roles);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToHome();
        }
    }
}
