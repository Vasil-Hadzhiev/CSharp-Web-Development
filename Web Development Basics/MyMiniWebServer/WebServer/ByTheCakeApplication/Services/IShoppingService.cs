namespace WebServer.ByTheCakeApplication.Services
{
    using Data.Models;
    using System.Collections.Generic;
    using ViewModels.Products;

    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<int> productIds);

        Order GetOrder(int id);
    }
}