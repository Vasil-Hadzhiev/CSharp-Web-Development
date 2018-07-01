namespace ModPanel.App.Controllers
{
    using Data;
    using ModPanel.Models;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;

    public abstract class BaseController : Controller
    {
        private const string GuestMenuHtml = @"
                    <ul class=""navbar-nav"">
                        <li class=""nav-item active"">
                            <a class=""nav-link"" href=""/users/login"">Login</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link"" href=""/users/register"">Register</a>
                        </li>
                    </ul>";

        private const string UserMenuHtml = @"
                     <ul class=""navbar-nav"">
                        <li class=""navbar-nav"">
                            <form action = ""/"" method=""get"" class=""form-inline my-2 my-lg-0"">
                                <input class=""form-control mr-sm-2"" type=""text"" name=""search"" placeholder=""Search"" aria-label=""Search"">
                                <button class=""btn btn-dark mr-3 my-2 my-sm-0"" type=""submit"">Search</button>
                            </form>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link"" href=""/posts/create"">New Post</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link"" href=""/users/logout"">Logout</a>
                        </li>
                     </ul>";

        private const string AdminMenuHtml = @"
                    <ul class=""navbar-nav mr-auto mt-2 mt-lg-0"">
                        <li class=""nav-item dropdown float-left"">
                            <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"" role=""button"" aria-haspopup=""true"" aria-expanded=""false"">Admin</a>
                            <div class=""dropdown-menu"">
                                <a class=""dropdown-item"" href=""/admin/log"">Log</a>
                                <a class=""dropdown-item"" href=""/admin/posts"">Posts</a>
                                <a class=""dropdown-item"" href=""/admin/users"">Users</a>
                            </div>
                        </li>
                    </ul>";

        protected BaseController()
        {
            this.Context = new ModPanelContext();
            
            this.Model.Data["show-error"] = "none";
        }

        protected ModPanelContext Context { get; set; }

        protected User CurrentUser { get; set; }

        protected IActionResult RedirectToHome() => this.RedirectToAction("/home/index");

        protected void ShowError(string errorMsg)
        {
            this.Model.Data["show-error"] = "block";
            this.Model.Data["error"] = errorMsg;
        }

        public override void OnAuthentication()
        {
            if (this.User.IsAuthenticated)
            {
                this.CurrentUser = this.Context
                    .Users
                    .FirstOrDefault(u => u.Email == this.User.Name);
            }

            if (!this.User.IsAuthenticated)
            {
                this.Model.Data["navMenu"] = GuestMenuHtml;
            }
            else if (!this.CurrentUser.IsAdmin)
            {
                this.Model.Data["navMenu"] = UserMenuHtml;
            }
            else if (this.CurrentUser.IsAdmin)
            {
                this.Model.Data["navMenu"] = AdminMenuHtml + UserMenuHtml;
            }

            base.OnAuthentication();
        }
    }
}
