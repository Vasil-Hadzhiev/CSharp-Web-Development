namespace BookLibrary.Web.Pages.Books
{
    using BookLibrary.Models;
    using BookLibrary.Web.Models;
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Linq;

    public class StatusModel : PageModel
    {
        public StatusModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }

        public List<BookStatusViewModel> BookRecords { get; set; }

        public string Title { get; set; }

        public void OnGet(int id)
        {
            this.Title = this.Context
                .Books
                .FirstOrDefault(b => b.Id == id)
                .Title;

            this.BookRecords = this.Context
                .BookRecords
                .Where(r => r.BookId == id)
                .Select(br => new BookStatusViewModel
                {
                    StartDate = br.StartDate,
                    EndDate = br.EndDate
                })
                .ToList();
        }
    }
}