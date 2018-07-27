namespace Library.Web.Pages.Authors
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class DetailsModel : PageModel
    {
        private readonly LibraryContext context;

        public DetailsModel(LibraryContext context)
        {
            this.context = context;
        }

        public string Author { get; set; }

        public ICollection<AuthorsDetailsViewModel> Books { get; set; }

        public void OnGet(int id)
        {
            this.Author = this.context
                .Authors
                .FirstOrDefault(a => a.Id == id)
                .Name;

            this.Books = this.context
                .Books
                .Where(b => b.AuthorId == id)
                .Select(b => new AuthorsDetailsViewModel
                {
                    BookId = b.Id,
                    Title = b.Title
                })
                .ToList();
        }
    }
}