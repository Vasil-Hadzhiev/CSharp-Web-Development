namespace WebServer.GameStoreApplication.Controllers
{
    using Server.Http;
    using Server.Http.Contracts;
    using Services;
    using Services.Contracts;
    using ViewModels.Account;

    public class AccountController : BaseController
    {
        private const string RegisterView = @"account\register";
        private const string LoginView = @"account\login";

        private IUserService users;

        public AccountController(IHttpRequest request)
            : base(request)
        {
            this.users = new UserService();
        }

        public IHttpResponse Register()
        {
            return this.FileViewResponse(RegisterView);
        }

        public IHttpResponse Register(RegisterViewModel model)
        {
            var isValid = this.ValidateModel(model);

            if (!isValid)
            {
                return this.FileViewResponse(RegisterView);
            }

            var email = model.Email;
            var name = model.FullName;
            var password = model.Password;

            var success = this.users.Create(email, name, password);

            if (success)
            {
                this.LoginUser(email);

                return this.RedirectResponse("/");
            }
            else
            {
                this.ShowError("E-mail already exists.");

                return this.FileViewResponse(RegisterView);
            }
        }

        public IHttpResponse Login()
        {
            return this.FileViewResponse(LoginView);
        }

        public IHttpResponse Login(LoginViewModel model)
        {
            var isValid = this.ValidateModel(model);

            if (!isValid)
            {
                return this.FileViewResponse(LoginView);
            }

            var email = model.Email;
            var password = model.Password;

            var success = this.users.Find(email, password);

            if (success)
            {
                this.LoginUser(email);

                return this.RedirectResponse("/");
            }
            else
            {
                this.ShowError("Invalid user details.");

                return this.FileViewResponse(LoginView);
            }
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();
            return this.RedirectResponse("/");
        }

        private void LoginUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
        }
    }
}