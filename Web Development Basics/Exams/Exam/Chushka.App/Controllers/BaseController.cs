namespace Chushka.App.Controllers
{
    using Data;
    using SoftUni.WebServer.Mvc.Controllers;
    using SoftUni.WebServer.Mvc.Interfaces;
    using System.Linq;

    public abstract class BaseController : Controller
    {
        private const string GuestHtmlBar = @"
                    <ul class=""navbar-nav"">
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/home/index"">Home</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/users/login"">Login</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/users/register"">Register</a>
                        </li>
                    </ul>";

        private const string AdminHtmlBar = @"
                    <ul class=""navbar-nav"">
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/home/index"">Home</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/products/create"">Create Product</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/orders/all"">All Orders</a>
                        </li>
                    </ul>
                    <ul class=""navbar-nav left-side"">
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/users/logout"">Logout</a>
                        </li>
                    </ul>";

        private const string UserHtmlBar = @"
                    <ul class=""navbar-nav right-side"">
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/home/index"">Home</a>
                        </li>
                    </ul>
                    <ul class=""navbar-nav left-side"">
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/users/logout"">Logout</a>
                        </li>
                    </ul>";

        protected BaseController()
        {
            this.Context = new ChushkaContext();
            
            this.ViewData.Data["show-error"] = "none";
        }

        protected ChushkaContext Context { get; set; }

        protected bool IsAdmin => this.User.IsAuthenticated && this.User.Roles.First() == "Admin";

        protected IActionResult RedirectToHome() => this.RedirectToAction("/home/index");

        protected void ShowError(string errorMsg)
        {
            this.ViewData.Data["show-error"] = "block";
            this.ViewData.Data["error"] = errorMsg;
        }

        public override void OnAuthentication()
        {
            if (this.User.IsAuthenticated)
            {
                var userRole = this.User.Roles.First();

                if (userRole == "Admin")
                {
                    this.ViewData.Data["menu"] = AdminHtmlBar;
                }
                else
                {
                    this.ViewData.Data["menu"] = UserHtmlBar;
                }
            }
            else
            {
                this.ViewData.Data["menu"] = GuestHtmlBar;
            }

            base.OnAuthentication();
        }
    }
}