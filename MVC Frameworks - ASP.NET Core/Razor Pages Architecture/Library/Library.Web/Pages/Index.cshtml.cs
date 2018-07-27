namespace Library.Web.Pages
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class IndexModel : PageModel
    {
        private readonly LibraryContext context;

        public IndexModel(LibraryContext context)
        {
            this.context = context;
        }

        public IEnumerable<AllBooksViewModel> Books { get; set; }

        public void OnGet()
        {
            this.Books = this.context
                .Books
                .Include(b => b.Author)
                .Select(b => new AllBooksViewModel
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