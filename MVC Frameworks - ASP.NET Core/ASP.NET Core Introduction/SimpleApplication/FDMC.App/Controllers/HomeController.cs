namespace FDMC.App.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using System.Linq;

    public class HomeController : Controller
    {
        public HomeController(FdmcContext context)
        {
            this.Context = context;
        }

        public FdmcContext Context { get; private set; }

        public IActionResult Index()
        {
            var cats = this.Context
                .Cats
                .Select(cat => new CatsConciseViewModel
                {
                    Id = cat.Id,
                    Name = cat.Name
                })
                .ToList();

            return View(cats);
        }
    }
}