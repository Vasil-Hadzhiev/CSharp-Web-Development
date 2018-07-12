namespace FDMC.App.Controllers
{
    using Data;
    using FDMC.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.BindingModels;
    using Models.ViewModels;
    using System.Linq;

    public class CatsController : Controller
    {
        public CatsController(FdmcContext context)
        {
            this.Context = context;
        }

        public FdmcContext Context { get; private set; }

        public IActionResult Details(int id)
        {
            var cat = this.Context
                .Cats
                .FirstOrDefault(c => c.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            var catModel = new CatDetailsViewModel
            {
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };

            return View(catModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CatCreatingBindingModel model)
        {
            var cat = new Cat
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };

            this.Context.Cats.Add(cat);
            this.Context.SaveChanges();

            //return RedirectToRoute(
            //    "default",
            //    new { controller = "Cats", action = "Details", id = model.Id });

            return RedirectToAction("Details", new { id = cat.Id });
        }
    }
}