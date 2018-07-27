namespace Library.Web.Models.BindingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BorrowMovieBindingModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Borrower")]
        public string Borrower { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.EndDate != null && this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start Date can't be after End Date");
            }
        }
    }
}