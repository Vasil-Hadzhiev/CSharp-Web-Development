namespace BookLibrary.Web.Pages
{
    using Data;
    using Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class SearchModel : PageModel
    {
        public SearchModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }

        public string SearchTerm { get; set; }

        public ICollection<SearchAuthorsViewModel> Authors { get; set; }

        public ICollection<SearchBooksViewModel> Books { get; set; }

        public void OnGet(string searchTerm)
        {
            this.SearchTerm = searchTerm;

            this.Authors = this.Context
                .Authors
                .Where(a => a.Name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                .Select(a => new SearchAuthorsViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList();

            this.Books = this.Context
                .Books
                .Where(b => b.Title.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
                .Select(b => new SearchBooksViewModel
                {
                    Id = b.Id,
                    Title = b.Title
                })
                .ToList();
        }
    }
}