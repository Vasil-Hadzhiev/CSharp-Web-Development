namespace BookLibrary.Web.Pages.Books
{
    using BookLibrary.Data;
    using BookLibrary.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AddModel : PageModel
    {
        public AddModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Author { get; set; }

        [BindProperty]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public IActionResult OnPostAddBook()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var author = GetOrCreateAuthor();

            var book = new Book
            {
                Title = this.Title,
                AuthorId = author.Id,
                CoverImage = this.ImageUrl,
                Description = this.Description
            };

            this.Context.Books.Add(book);
            this.Context.SaveChanges();

            return this.RedirectToPage("/Books/Details", new { id = book.Id });
        }

        public IActionResult OnPostCancel()
        {
            return this.RedirectToPage("/Index");
        }

        private Author GetOrCreateAuthor()
        {
            var author = this.Context
                .Authors
                .FirstOrDefault(a => a.Name == this.Author);

            if (author == null)
            {
                author = new Author
                {
                    Name = this.Author
                };

                this.Context.Authors.Add(author);
                this.Context.SaveChanges();
            }

            return author;
        }
    }
}