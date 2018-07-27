namespace Library.Web.Pages.Books
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class StatusModel : PageModel
    {
        private readonly LibraryContext context;

        public StatusModel(LibraryContext context)
        {
            this.context = context;
        }

        public List<BookStatusViewModel> BookRecords { get; set; }

        public string Title { get; set; }

        public void OnGet(int id)
        {
            this.Title = this.context
                .Books
                .FirstOrDefault(b => b.Id == id)
                .Title;

            var currentBookRecords = this.context
                .BookRecords
                .Where(r => r.BookId == id)
                .Select(br => br.RecordId)
                .ToList();

            this.BookRecords = this.context
                .Records
                .Where(r => currentBookRecords.Contains(r.Id))
                .Select(r => new BookStatusViewModel
                {
                    StartDate = r.StartDate,
                    EndDate = r.EndDate
                })
                .ToList();
        }
    }
}