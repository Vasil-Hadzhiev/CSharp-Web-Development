namespace KittenApp.Web.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model.Data["username"] = this.User.Name;
                this.Model.Data["anonymousDisplay"] = "none";
                this.Model.Data["authDisplay"] = "block";
            }
            else
            {
                this.Model.Data["anonymousDisplay"] = "block";
                this.Model.Data["authDisplay"] = "none";
            }

            return this.View();
        }
    }
}