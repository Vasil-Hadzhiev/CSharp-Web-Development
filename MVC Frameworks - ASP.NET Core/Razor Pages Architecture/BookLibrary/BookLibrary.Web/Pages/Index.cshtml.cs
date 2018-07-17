namespace BookLibrary.Web.Pages
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class IndexModel : PageModel
    {
        public IndexModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }

        public IEnumerable<BooksViewModel> Books { get; set; }

        public void OnGet()
        {
            this.Books = this.Context
                .Books
                .Include(b => b.Author)
                .Select(b => new BooksViewModel
                {
                    BookId = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    Author = b.Author.Name,
                    Status = b.Status
                })
                .ToList();
        }
    }
}