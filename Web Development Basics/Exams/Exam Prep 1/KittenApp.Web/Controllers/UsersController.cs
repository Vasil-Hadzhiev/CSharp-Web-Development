namespace KittenApp.Web.Controllers
{
    using Models;
    using Services;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

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
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid register details.");
                return this.View();
            }

            var username = model.Username;
            var email = model.Email;
            var password = model.Password;

            var user = this.users.Create(username, email, password);
            if (user == null)
            {
                this.ShowError("Username already taken or empty fields.");
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
        public IActionResult Login(LoginViewModel model)
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
                this.ShowError("Invalid username or password.");
                return this.View();
            }

            this.SignIn(username, user.Id);

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