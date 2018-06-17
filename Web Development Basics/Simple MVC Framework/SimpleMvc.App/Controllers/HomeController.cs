namespace SimpleMvc.App.Controllers
{
    using Framework.Controllers;
    using Framework.Contracts;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}