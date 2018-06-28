namespace MeTube.App.Services.Contracts
{
    using MeTube.Models;
    using Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        User Create(string username, string password, string email);

        User Find(string username, string password);

        List<UserProfileViewModel> UserTubes(int id);
    }
}