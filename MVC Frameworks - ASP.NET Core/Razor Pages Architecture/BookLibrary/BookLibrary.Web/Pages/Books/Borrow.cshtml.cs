namespace BookLibrary.Web.Pages.Books
{
    using BookLibrary.Models;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BorrowModel : PageModel
    {
        public BorrowModel(BookLibraryContext context)
        {
            this.Context = context;

            this.Borrowers = new List<string>();
        }

        public BookLibraryContext Context { get; set; }

        public List<string> Borrowers { get; set; }

        [BindProperty]
        public string Borrower { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime? EndDate { get; set; }

        public IActionResult OnGet(int id)
        {
            var book = this.Context
                .Books
                .FirstOrDefault(b => b.Id == id);

            if (book == null || book.Status == "Borrowed")
            {
                return this.RedirectToPage("/Index");
            }

            this.Borrowers = this.Context
                .Borrowers
                .Select(b => b.Name)
                .ToList();

            return this.Page();
        }

        public IActionResult OnPostBorrow(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var book = this.Context
                .Books
                .FirstOrDefault(b => b.Id == id);

            var borrower = this.Context
                .Borrowers
                .FirstOrDefault(b => b.Name == this.Borrower);

            var borrowerBook = new BorrowersBooks
            {
                BookId = book.Id,
                BorrowerId = borrower.Id
            };

            book.Status = "Borrowed";

            if (this.StartDate > this.EndDate)
            {
                return this.Page();
            }

            var bookRecord = new BookRecord
            {
                BookId = book.Id,
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };

            this.Context.BookRecords.Add(bookRecord);
            this.Context.SaveChanges();

            return this.RedirectToPage("/Index");
        }
    }
}