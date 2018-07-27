namespace Library.Web.Pages.Books
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels;
    using System;
    using System.Linq;

    public class DetailsModel : PageModel
    {
        private readonly LibraryContext context;

        public DetailsModel(LibraryContext context)
        {
            this.context = context;
        }

        public BookDetailsViewModel BookDetails { get; set; }

        public IActionResult OnGet(int id)
        {
            this.BookDetails = this.context.Books
                .Where(b => b.Id == id)
                .Select(b => new BookDetailsViewModel()
                {
                    Author = b.Author.Name,
                    CoverImg = b.CoverImg,
                    Title = b.Title,
                    Description = b.Description,
                    Status = b.Status
                })
                .FirstOrDefault();

            if (this.BookDetails == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }

        public IActionResult OnPostReturnBook(int id)
        {
            var book = this.context
                .Books
                .FirstOrDefault(b => b.Id == id);

            var borrower = this.context.Borrowers
               .Include(b => b.BorrowedBooks)
               .FirstOrDefault(b => b.BorrowedBooks.FirstOrDefault(bb => bb.BookId == id) != null);

            if (borrower == null)
            {
                return this.RedirectToPage("/Index");
            }

            var borrowerBook = borrower.BorrowedBooks
                .FirstOrDefault(b => b.BookId == id);

            borrower.BorrowedBooks.Remove(borrowerBook);

            book.Status = "At home";

            var bookRecord = this.context
                .BookRecords
                .FirstOrDefault(br => br.BookId == id);

            var record = this.context
                .Records
                .FirstOrDefault(r => r.Id == bookRecord.RecordId);

            record.EndDate = DateTime.Today;

            this.context.SaveChanges();

            return this.RedirectToPage("/Index");
        }

        public IActionResult OnPostBorrowBook(int id)
        {
            return this.RedirectToPage("/Books/Borrow", new { id });
        }
    }
}