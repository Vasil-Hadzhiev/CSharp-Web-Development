namespace MeTube.App.Controllers
{
    using Data;
    using MeTube.Models;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;

    public abstract class BaseController : Controller
    {
        private const string GuestMenuHtml = @"
                    <li class=""nav-item active col-md-4"">
                        <a class=""nav-link h5"" href=""/"">Home</a>
                    </li>
                    <li class=""nav-item active col-md-4"">
                        <a class=""nav-link h5"" href=""/users/login"">Login</a>
                    </li>
                    <li class=""nav-item active col-md-4"">
                        <a class=""nav-link h5"" href=""/users/register"">Register</a>
                    </li>";

        private const string UserMenuHtml = @"
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/"">Home</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/users/profile"">Profile</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/tube/upload"">Upload</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/users/logout"">Logout</a>
                    </li>";

        protected BaseController()
        {
            this.Context = new MeTubeContext();

            this.Model.Data["anonymousDisplay"] = "block";
            this.Model.Data["authDisplay"] = "none";
            this.Model.Data["show-error"] = "none";
        }

        protected User CurrentUser { get; set; }

        protected MeTubeContext Context { get; set; }

        protected IActionResult RedirectToHome() => this.RedirectToAction("/home/index");

        protected void ShowError(string errorMsg)
        {
            this.Model.Data["show-error"] = "block";
            this.Model.Data["error"] = errorMsg;
        }

        public override void OnAuthentication()
        {
            this.Model.Data["navMenu"] = this.User.IsAuthenticated ? UserMenuHtml : GuestMenuHtml;

            if (this.User.IsAuthenticated)
            {
                this.CurrentUser = this.Context
                    .Users
                    .FirstOrDefault(u => u.Username == this.User.Name);
            }

            base.OnAuthentication();
        }
    }
}