namespace Library.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddBorrowerBindingModel
    {
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(50, ErrorMessage = "Name can't be greater than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address field is required")]
        [MaxLength(50, ErrorMessage = "Address can't be greater than 50 characters")]
        public string Address { get; set; }
    }
}
