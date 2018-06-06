namespace WebServer.ByTheCakeApplication.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Orders;

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

        //TODO: Get products for the given order
        public IEnumerable<OrderDetailView> GetProducts(int id)
        {
            using (var db = new ByTheCakeContext())
            {
                var orderProduct = db
                    .OrderProduct
                    .Where(op => op.OrderId == id);

                var products = orderProduct
                    .Select(op => new OrderDetailView
                    {
                        Name = op.Product.Name,
                        Price = op.Product.Price
                    })
                    .ToList();

                return products;
            }
        }
    }
}