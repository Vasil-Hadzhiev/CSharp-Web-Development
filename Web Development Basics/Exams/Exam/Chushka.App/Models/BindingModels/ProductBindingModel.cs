namespace Chushka.App.Models.BindingModels
{
    public class ProductBindingModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int ProductTypeId { get; set; }
    }
}