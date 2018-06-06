﻿namespace WebServer.ByTheCakeApplication.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            this.Orders = new List<OrderProduct>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }

        public List<OrderProduct> Orders { get; set; }
    }
}