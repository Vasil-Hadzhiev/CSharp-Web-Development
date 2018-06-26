namespace KittenApp.Web.Services.Contracts
{
    using KittenApp.Models;

    public interface IUsersService
    {
        User Create(string username, string email, string password);

        User Find(string username, string password);
    }
}