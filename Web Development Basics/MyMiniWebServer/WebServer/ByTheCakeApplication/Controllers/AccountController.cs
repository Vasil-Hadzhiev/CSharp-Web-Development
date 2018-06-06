namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using System;
    using ViewModels;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private readonly IUserService users;

        public AccountController()
        {
            this.users = new UserService();
        }

        //GET Register
        public IHttpResponse Register()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(@"account\register");
        }

        //POST Register
        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel model)
        {
            this.SetDefaultViewData();

            var username = model.Username;
            var password = model.Password;
            var confirmPassword = model.Password;

            //Model validation
            if (username.Length < 3
                || password.Length < 3
                || confirmPassword != password)
            {
                this.AddError("Invalid user details");

                return this.FileViewResponse(@"account\register");
            }

            //TODO: Password HASH
            var success = this.users.Create(username, password);

            if (success)
            {
                this.LoginUser(req, username);

                return new RedirectResponse("/");
            }
            else
            {
                this.AddError("This username is taken");

                return this.FileViewResponse(@"account\register");
            }
        }

        //GET Login
        public IHttpResponse Login()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(@"account\login");
        }

        //POST Login
        public IHttpResponse Login(IHttpRequest req, LoginViewModel model)
        {
            var username = model.Username;
            var password = model.Password;

            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password))
            {
                this.AddError("You have empty fields");

                return this.FileViewResponse(@"account\login");
            }

            var success = this.users.Find(username, password);

            if (success)
            {
                this.LoginUser(req, model.Username);

                return new RedirectResponse("/");
            }
            else
            {
                this.AddError("Invalid user details");

                return this.FileViewResponse(@"account\login");
            }
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            if (!req.Session.Contains(SessionStore.CurrentUserKey))
            {
                throw new InvalidOperationException("There is no logged in user.");
            }

            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var profile = this.users.Profile(username);

            if (profile == null)
            {
                throw new InvalidOperationException($"The user {username} could not be found in the database.");
            }

            this.ViewData["username"] = profile.Username;
            this.ViewData["registrationDate"] = profile.RegistrationDate.ToShortDateString();
            this.ViewData["totalOrders"] = profile.TotalOrders.ToString();

            return this.FileViewResponse(@"account\profile");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }

        private void SetDefaultViewData() => this.ViewData["authDisplay"] = "none";

        private void LoginUser(IHttpRequest req, string username)
        {
            req.Session.Add(SessionStore.CurrentUserKey, username);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }
    }
}