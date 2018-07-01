namespace ModPanel.App.Controllers
{
    using Models.BindingModels;
    using ModPanel.Models;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
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

            var email = model.Email;
            var password = model.Password;
            var position = model.Position;

            if (this.Context.Users.FirstOrDefault(u => u.Email == email) != null)
            {
                this.ShowError("User already exists!");
                return this.View();
            }

            var user = new User
            {
                Email = email,
                Password = PasswordUtilities.GetPasswordHash(password),
                Position = position
            };

            if (!this.Context.Users.Any())
            {
                user.IsAdmin = true;
                user.IsApproved = true;
            }

            using (this.Context)
            {
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
                this.SignIn(user.Email, user.Id);
            }

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

        [HttpGet]
        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToHome();
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
                this.ShowError("Invalid login details.");
                return this.View();
            }

            var email = model.Email;
            var password = PasswordUtilities.GetPasswordHash(model.Password);

            var user = this.Context
                    .Users
                    .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                this.ShowError("User does not exist!");
                return this.View();
            }

            if (!user.IsApproved)
            {
                this.ShowError("You must wait for your registration to be approved!");
                return this.View();
            }

            this.SignIn(user.Email, user.Id);

            return this.RedirectToHome();
        }
    }
}