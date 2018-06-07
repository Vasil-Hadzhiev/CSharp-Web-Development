namespace WebServer.ByTheCakeApplication.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Orders;
    using WebServer.ByTheCakeApplication.ViewModels.Products;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int> productIds)
        {
            using (var db = new ByTheCakeContext())
            {
                var order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Products = productIds
                        .Select(id => new OrderProduct
                        {
                            ProductId = id
                        })
                        .ToList()
                };

                db.Add(order);
                db.SaveChanges();
            }
        }

        public Order GetOrder(int id)
        {
            using (var db = new ByTheCakeContext())
            {
                var order = db.Orders
                    .FirstOrDefault(o => o.Id == id);

                return order;
            }
        }      
    }
}