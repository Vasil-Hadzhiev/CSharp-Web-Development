namespace WebServer.GameStoreApplication.Services.Contracts
{
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using ViewModels.Admin;

    public interface IGameService
    {
        bool Create(
            string title,
            string description,
            string image,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate);

        IEnumerable<AdminListGamesViewModel> AllGames();

        Game GetGame(int id);

        void Edit(
            int id,
            string title,
            string description,
            string image,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate);
    }
}