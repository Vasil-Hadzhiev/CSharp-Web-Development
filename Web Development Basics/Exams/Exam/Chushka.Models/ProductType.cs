namespace Chushka.Models
{
    using System.Collections.Generic;

    public class ProductType
    {
        public ProductType()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}