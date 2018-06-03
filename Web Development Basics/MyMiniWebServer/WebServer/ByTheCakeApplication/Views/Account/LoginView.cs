namespace WebServer.ByTheCakeApplication.Views.Account
{
    using Server.Contracts;

    public class LoginView : IView
    {
        public string View()
        {
            var loginView =
                "<form method=\"POST\">" +
                "<h1>Login</h1><br /> " +
                "<label>Name</label>" +
                "<input name=\"username\" type=\"text\" />" +
                "<label>Password</label>" +
                "<input name=\"password\" type=\"password\" />" +
                "<input type=\"submit\" value=\"Login\" />" +
                "</form>";

            return loginView;
        }
    }
}