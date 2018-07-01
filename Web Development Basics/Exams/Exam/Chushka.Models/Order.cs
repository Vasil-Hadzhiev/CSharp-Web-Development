namespace Chushka.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string OrderId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public User Client { get; set; }

        public int ClientId { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}