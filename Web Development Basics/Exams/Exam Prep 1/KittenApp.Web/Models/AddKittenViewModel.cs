namespace KittenApp.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddKittenViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }
    }
}