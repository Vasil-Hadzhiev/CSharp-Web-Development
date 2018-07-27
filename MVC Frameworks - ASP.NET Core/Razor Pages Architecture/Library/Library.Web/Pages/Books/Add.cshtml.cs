namespace Library.Web.Pages.Books
{
    using Data;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.BindingModels;
    using System.Linq;

    public class AddModel : PageModel
    {
        private readonly LibraryContext context;

        public AddModel(LibraryContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public AddBookBindingModel BookModel { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var author = this.context
                .Authors
                .FirstOrDefault(a => a.Name == this.BookModel.Author);

            if (author == null)
            {
                author = new Author()
                {
                    Name = this.BookModel.Author
                };

                this.context.Authors.Add(author);
                this.context.SaveChanges();
            }

            var book = new Book()
            {
                AuthorId = author.Id,
                CoverImg = this.BookModel.CoverImg,
                Title = this.BookModel.Title,
                Description = this.BookModel.Description
            };

            this.context.Books.Add(book);
            this.context.SaveChanges();

            return this.RedirectToPage("/Books/Details", new { id = book.Id });
        }
    }
}