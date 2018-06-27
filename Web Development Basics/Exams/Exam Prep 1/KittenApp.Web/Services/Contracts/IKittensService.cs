namespace KittenApp.Web.Services.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IKittensService
    {
        bool Add(string name, int age, string breed);

        List<string> All();
    }
}