namespace Library.Web.Controllers
{
    using Data;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.BindingModels;
    using Models.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MoviesController : Controller
    {
        private readonly LibraryContext context;

        public MoviesController(LibraryContext context)
        {
            this.context = context;
        }

        public MovieDetailsViewModel DetailsModel { get; set; }

        public List<AllMoviesViewModel> AllMovies { get; set; }

        public MovieStatusViewModel MovieStatus { get; set; }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddMovieBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var director = this.context
                .Directors
                .FirstOrDefault(d => d.Name == model.Director);

            if (director == null)
            {
                director = new Director()
                {
                    Name = model.Director
                };

                this.context.Directors.Add(director);
                this.context.SaveChanges();
            }

            var movie = new Movie()
            {
                DirectorId = director.Id,
                CoverImg = model.CoverImg,
                Title = model.Title,
                Description = model.Description
            };

            this.context.Movies.Add(movie);
            this.context.SaveChanges();

            return this.RedirectToAction("Details", new { id = movie.Id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            this.DetailsModel = this.context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailsViewModel
                {
                    Id = id,
                    Director = m.Director.Name,
                    CoverImg = m.CoverImg,
                    Title = m.Title,
                    Description = m.Description,
                    Status = m.Status
                })
                .FirstOrDefault();

            if (this.DetailsModel == null)
            {
                return this.NotFound();
            }


            return this.View(this.DetailsModel);
        }

        [HttpGet]
        public IActionResult All()
        {
            this.AllMovies = this.context.Movies
                .Select(b => new AllMoviesViewModel()
                {
                    MovieId = b.Id,
                    DirectorId = b.DirectorId,
                    Director = b.Director.Name,
                    Title = b.Title,
                    Status = b.Status
                })
                .ToList();

            return this.View(this.AllMovies);
        }

        [HttpGet]
        public IActionResult Borrow(int id)
        {
            var movie = this.context
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie.Status == "Borrowed" || movie == null)
            {
                return this.RedirectToAction("All", "Movies");
            }

            this.ViewBag.Names = this.context.Borrowers.Select(b => b.Name).ToArray();

            return this.View();
        }

        [HttpPost]
        public IActionResult Borrow(BorrowMovieBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var movie = this.context
                .Movies
                .FirstOrDefault(m => m.Id == model.Id);

            if (movie == null)
            {
                return this.RedirectToAction("All");
            }

            var borrower = this.context
                .Borrowers
                .FirstOrDefault(b => b.Name == model.Borrower);

            if (borrower == null)
            {
                return this.View();
            }

            var borrowerMovie = new BorrowersMovies
            {
                MovieId = model.Id,
                BorrowerId = borrower.Id
            };

            borrower.BorrowedMovies.Add(borrowerMovie);

            movie.Status = "Borrowed";

            var movieRecord = new MoviesRecords
            {
                MovieId = model.Id,
                Record = new Record
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                }
            };

            movie.Records.Add(movieRecord);

            this.context.SaveChanges();

            return this.RedirectToAction("All");
        }

        [HttpPost]
        public IActionResult Return(int id)
        {
            var movie = this.context
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return this.RedirectToAction("All");
            }

            var borrower = this.context.Borrowers
               .Include(b => b.BorrowedMovies)
               .FirstOrDefault(b => b.BorrowedMovies.FirstOrDefault(bm => bm.MovieId == id) != null);

            var borrowerMovie = borrower.BorrowedMovies
                .FirstOrDefault(bm => bm.MovieId == id);

            borrower.BorrowedMovies.Remove(borrowerMovie);

            movie.Status = "At home";

            var movieRecord = this.context
                .MovieRecords
                .FirstOrDefault(br => br.MovieId == id);

            var record = this.context
                .Records
                .FirstOrDefault(r => r.Id == movieRecord.RecordId);

            record.EndDate = DateTime.Today;

            this.context.SaveChanges();

            return this.RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Status(int id)
        {
            var movie = this.context
                .Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return this.RedirectToAction("All");
            }

            this.MovieStatus = new MovieStatusViewModel
            {
                Title = movie.Title
            };

            var currentMovieRecords = this.context
                .MovieRecords
                .Where(mr => mr.MovieId == id)
                .Select(mr => mr.RecordId)
                .ToList();

            this.MovieStatus.MovieRecords = this.context
                .Records
                .Where(r => currentMovieRecords.Contains(r.Id))
                .Select(r => new MovieRecordsViewModel
                {
                    StartDate = r.StartDate,
                    EndDate = r.EndDate
                })
                .ToList();

            return this.View(this.MovieStatus);
        }
    }
}