namespace KittenApp.Web.Controllers
{
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public abstract class BaseController : Controller
    {
        private const string UsersTopMenuHtml = @"
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/kittens/all"">All Kittens</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/kittens/add"">Add Kitten</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/logout"">Logout</a>
                </li>";

        private const string GuestsTopMenuHtml = @"
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/login"">Login</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/register"">Register</a>
                </li>";

        protected BaseController()
        {
            this.Model.Data["anonymousDisplay"] = "block";
            this.Model.Data["authDisplay"] = "none";
            this.Model.Data["show-error"] = "none";            
        }

        protected IActionResult RedirectToHome() => this.RedirectToAction("/home/index");

        protected void ShowError(string errorMsg)
        {
            this.Model.Data["show-error"] = "block";
            this.Model.Data["error"] = errorMsg;
        }

        public override void OnAuthentication()
        {
            this.Model.Data["menu"] = this.User.IsAuthenticated ? UsersTopMenuHtml : GuestsTopMenuHtml;

            base.OnAuthentication();
        }
    }
}