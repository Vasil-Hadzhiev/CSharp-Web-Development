namespace Library.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class DirectorsController : Controller
    {
        private readonly LibraryContext context;

        public DirectorsController(LibraryContext context)
        {
            this.context = context;
        }

        public DirectorsDetailsViewModel DetailsModel { get; set; }

        public IActionResult Details(int id)
        {
            var director = this.context
                .Directors
                .FirstOrDefault(d => d.Id == id);

            if (director == null)
            {
                return this.RedirectToAction("All", "Movies");
            }

            this.DetailsModel = new DirectorsDetailsViewModel
            {
                Director = director.Name
            };

            this.DetailsModel.Movies = this.context
                .Movies
                .Where(m => m.DirectorId == id)
                .Select(m => new DirectorsMoviesViewModel
                {
                    MovieId = m.Id,
                    Title = m.Title
                })
                .ToList();

            return this.View(this.DetailsModel);
        }
    }
}