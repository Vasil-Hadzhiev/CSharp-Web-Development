namespace BookLibrary.Web.Pages.Borrowers
{
    using System.Linq;
    using BookLibrary.Models;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class AddModel : PageModel
    {
        public AddModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Address { get; set; }

        public IActionResult OnPostAddBorrower()
        {
            if (!this.ModelState.IsValid || 
                this.Context.Borrowers.Any(b => b.Name == this.Name))
            {
                return this.Page();
            }

            var borrower = new Borrower()
            {
                Name = this.Name,
                Address = this.Address
            };

            this.Context.Add(borrower);
            this.Context.SaveChanges();

            return this.RedirectToPage("/Index");
        }

        public IActionResult OnPostCancel()
        {
            return this.RedirectToPage("/Index");
        }
    }
}