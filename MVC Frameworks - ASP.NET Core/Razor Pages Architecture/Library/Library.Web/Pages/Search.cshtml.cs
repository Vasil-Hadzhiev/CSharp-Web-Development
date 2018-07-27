namespace Library.Web.Pages
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SearchModel : PageModel
    {
        private readonly LibraryContext context;

        public SearchModel(LibraryContext context)
        {
            this.context = context;
        }

        public string SearchTerm { get; set; }

        public ICollection<SearchAuthorsViewModel> Authors { get; set; }

        public ICollection<SearchBooksViewModel> Books { get; set; }

        public ICollection<SearchDirectorsViewModel> Directors { get; set; }

        public ICollection<SearchMoviesViewModel> Movies { get; set; }

        public void OnGet(string searchTerm)
        {
            this.SearchTerm = searchTerm;

            this.Authors = this.context
                .Authors
                .Where(a => a.Name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                .Select(a => new SearchAuthorsViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList();

            this.Books = this.context
                .Books
                .Where(b => b.Title.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                .Select(b => new SearchBooksViewModel
                {
                    Id = b.Id,
                    Title = b.Title
                })
                .ToList();

            this.Movies = this.context
                .Movies
                .Where(m => m.Title.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                .Select(m => new SearchMoviesViewModel
                {
                    Id = m.Id,
                    Title = m.Title
                })
                .ToList();

            this.Directors = this.context
                .Directors
                .Where(d => d.Name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                .Select(d => new SearchDirectorsViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();
        }
    }
}