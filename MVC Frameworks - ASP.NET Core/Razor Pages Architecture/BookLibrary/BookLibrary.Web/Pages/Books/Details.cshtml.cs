namespace BookLibrary.Web.Pages.Books
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Linq;

    public class DetailsModel : PageModel
    {
        public DetailsModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public IActionResult OnGet(int id)
        {
            var book = this.Context
                .Books
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return this.NotFound();
            }

            this.Title = book.Title;
            this.Description = book.Description;
            this.ImageUrl = book.CoverImage;
            this.Author = this.Context
                .Authors
                .FirstOrDefault(a => a.Id == book.AuthorId)
                .Name;
            this.Status = book.Status;

            return this.Page();
        }

        public IActionResult OnPostBorrowBook(int id)
        {
            return this.RedirectToPage("/Books/Borrow", new { id = id });
        }

        public IActionResult OnPostReturnBook(int id)
        {
            var book = this.Context
                .Books
                .FirstOrDefault(b => b.Id == id);

            var bookId = book.Id;

            foreach (var item in this.Context.BorrowersBooks)
            {
                if (item.BookId == id)
                {
                    this.Context.BorrowersBooks.Remove(item);
                }
            }

            book.Status = "At home";
            this.Context.SaveChanges();

            return this.RedirectToPage("/Index");
        }

        public IActionResult OnPostBookStatus(int id)
        {
            return this.RedirectToPage("/Books/Status", new { id = id });
        }
    }
}