namespace Library.Web.Pages.Books
{
    using Data;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.BindingModels;
    using System.Collections.Generic;
    using System.Linq;

    public class BorrowModel : PageModel
    {
        private readonly LibraryContext context;

        public BorrowModel(LibraryContext context)
        {
            this.context = context;
            this.Borrowers = new List<string>();
        }

        [BindProperty]
        public BorrowBookBindingModel BorrowBookModel { get; set; }

        public List<string> Borrowers { get; set; }

        public IActionResult OnGet(int id)
        {
            var book = this.context
                .Books
                .FirstOrDefault(b => b.Id == id);

            if (book == null || book.Status == "Borrowed")
            {
                return this.RedirectToPage("/Index");
            }

            this.Borrowers = this.context
                .Borrowers
                .Select(b => b.Name)
                .ToList();

            return this.Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var book = this.context
                .Books
                .FirstOrDefault(b => b.Id == id);

            var borrower = this.context
                .Borrowers
                .FirstOrDefault(b => b.Name == this.BorrowBookModel.Borrower);

            var borrowerBook = new BorrowersBooks
            {
                BookId = book.Id,
                BorrowerId = borrower.Id
            };

            borrower.BorrowedBooks.Add(borrowerBook);

            book.Status = "Borrowed";

            var bookRecord = new BooksRecords
            {
                BookId = id,
                Record = new Record
                {
                    StartDate = this.BorrowBookModel.StartDate,
                    EndDate = this.BorrowBookModel.EndDate
                }
            };

            book.Records.Add(bookRecord);

            this.context.SaveChanges();

            return this.RedirectToPage("/Index");
        }
    }
}