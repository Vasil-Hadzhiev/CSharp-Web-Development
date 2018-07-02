namespace Chushka.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProductBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int ProductTypeId { get; set; }
    }
}