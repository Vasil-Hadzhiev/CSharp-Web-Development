namespace SimpleMvc.App.Services.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        bool Register(string username, string password);

        IEnumerable<UserViewModel> All();

        UserViewModel GetUserById(int id);
    }
}