namespace MeTube.App.Controllers
{
    using Models;
    using Services;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System;
    using System.Linq;

    public class UsersController : BaseController
    {
        private readonly IUsersService users;

        public UsersController()
        {
            this.users = new UsersService();
        }

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
        public IActionResult Register(RegisterUserViewModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid register details.");
                return this.View();
            }

            var username = model.Username;
            var password = model.Password;
            var email = model.Email;

            var user = this.users.Create(username, password, email);
            if (user == null)
            {
                this.ShowError("User already exists.");
                return this.View();
            }

            this.SignIn(username, user.Id);

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
        public IActionResult Login(LoginUserViewModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid login details.");
                return this.View();
            }

            var username = model.Username;
            var password = model.Password;

            var user = this.users.Find(username, password);
            if (user == null)
            {
                this.ShowError("User does not exist");
                return this.View();
            }

            this.SignIn(username, user.Id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            this.Model.Data["username"] = this.CurrentUser.Username;
            this.Model.Data["email"] = this.CurrentUser.Email;

            var tubes = this.users.UserTubes(this.CurrentUser.Id);

            var tubesAsString = tubes
                .Select(t => $@"
                        <tr>
                            <td>{t.Id}</td>
                            <td>{t.Title}</td>
                            <td>{t.Author}</td>
                            <td><a href=""/tube/details?id={t.Id}"">Details</a></td>
                        </tr>")
                 .ToList();

            var tubesResult = string.Join(Environment.NewLine, tubesAsString);

            this.Model.Data["tubes"] = tubesResult;

            return this.View();
        }
    }
}