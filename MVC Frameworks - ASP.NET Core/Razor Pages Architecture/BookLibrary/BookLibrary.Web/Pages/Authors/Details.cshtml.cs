namespace BookLibrary.Web.Pages.Authors
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class DetailsModel : PageModel
    {
        public DetailsModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }

        public string Author { get; set; }

        public ICollection<AuthorsBooksViewModel> Books { get; set; }

        public void OnGet(int id)
        {
            this.Author = this.Context
                .Authors
                .FirstOrDefault(a => a.Id == id)
                .Name;

            this.Books = this.Context
                .Books
                .Where(b => b.AuthorId == id)
                .Select(b => new AuthorsBooksViewModel
                {
                    BookId = b.Id,
                    Title = b.Title
                })
                .ToList();
        }
    }
}