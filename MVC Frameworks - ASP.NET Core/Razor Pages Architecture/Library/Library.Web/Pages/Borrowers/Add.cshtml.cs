namespace Library.Web.Pages.Borrowers
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
        public AddBorrowerBindingModel BorrowerModel { get; set; }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid ||
                this.context.Borrowers.Any(b => b.Name == this.BorrowerModel.Name))
            {
                return this.Page();
            }

            var borrower = new Borrower()
            {
                Name = this.BorrowerModel.Name,
                Address = this.BorrowerModel.Address
            };

            this.context.Borrowers.Add(borrower);
            this.context.SaveChanges();

            return this.RedirectToPage("/Index");
        }
    }
}