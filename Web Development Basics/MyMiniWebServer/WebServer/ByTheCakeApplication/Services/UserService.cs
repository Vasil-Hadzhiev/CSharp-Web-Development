namespace WebServer.ByTheCakeApplication.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Linq;
    using ViewModels.Account;
    using System.Collections.Generic;
    using WebServer.ByTheCakeApplication.ViewModels.Orders;

    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var db = new ByTheCakeContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                var user = new User
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                };

                db.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(string username, string password)
        {
            using (var db = new ByTheCakeContext())
            {
                return db
                    .Users
                    .Any(u => u.Username == username && u.Password == password);
            }
        }

        public ProfileViewModel Profile(string username)
        {
            using (var db = new ByTheCakeContext())
            {
                return db
                    .Users
                    .Where(u => u.Username == username)
                    .Select(u => new ProfileViewModel
                    {
                        Username = u.Username,
                        RegistrationDate = u.RegistrationDate,
                        TotalOrders = u.Orders.Count()
                    })
                    .FirstOrDefault();
            }
        }

        public int? GetUserId(string username)
        {
            using (var db = new ByTheCakeContext())
            {
                var id = db
                    .Users
                    .Where(u => u.Username == username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                return id != 0 ? (int?)id : null;
            }
        }

        //TODO: Maybe create new service for orders
        public IEnumerable<OrderListingViewModel> Orders(string username)
        {
            using (var db = new ByTheCakeContext())
            {
                var userId = this.GetUserId(username);

                var ordersQuery = db.Orders.Where(o => o.UserId == userId);

                return ordersQuery
                    .Select(o => new OrderListingViewModel
                    {
                        Id = o.Id,
                        CreatedOn = o.CreationDate.ToShortDateString(),
                        Sum = o.Products.Sum(p => p.Product.Price)
                    })
                    .ToList();
            }
        }
    }
}