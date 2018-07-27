namespace Library.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddBookBindingModel
    {
        [Required(ErrorMessage = "Title field is required")]
        [MaxLength(50, ErrorMessage = "Title can't be grater than 50 characters")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Image Url")]
        [Url]
        public string CoverImg { get; set; }

        [Required(ErrorMessage = "Author field is required")]
        [MaxLength(50, ErrorMessage = "Author name can't be grater than 50 characters")]
        public string Author { get; set; }
    }
}