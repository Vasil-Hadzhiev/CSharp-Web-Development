namespace WebServer.ByTheCakeApplication.Services
{
    using System.Collections.Generic;
    using ViewModels.Account;
    using ViewModels.Orders;

    public interface IUserService
    {
        bool Create(string username, string password);

        bool Find(string username, string password);

        ProfileViewModel Profile(string username);

        int? GetUserId(string username);

        IEnumerable<OrderListingViewModel> Orders(string username);
    }
}