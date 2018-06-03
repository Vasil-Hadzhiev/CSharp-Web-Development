namespace WebServer.ByTheCakeApplication.Controllers
{
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using ViewModels;
    using Views.Account;

    public class AccountController
    {
        private const string UserLoginToken = "${0}_UserLoggedInToken_$";

        //GET Login
        public IHttpResponse Login()
        {
            var result = new ViewResponse(HttpStatusCode.Ok, new LoginView());

            return result;
        }

        //POST Login
        public IHttpResponse Login(IHttpRequest request, LoginViewModel model )
        {
            SaveUserInSession(request, model.Username);
            return new RedirectResponse("/");
        }

        private static void SaveUserInSession(IHttpRequest request, string username)
        {
            request.Session.Add(nameof(UserLoginToken), string.Format(UserLoginToken, username));
        }
    }
}