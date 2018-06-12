namespace WebServer.GameStoreApplication.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Admin;

    public class GameService : IGameService
    {
        public bool Create(
            string title,
            string description,
            string image,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate)
        {
            using (var db = new GameStoreContext())
            {
                if (db.Games.Any(g => g.Title == title))
                {
                    return false;
                }

                var game = new Game
                {
                    Title = title,
                    Description = description,
                    Image = image,
                    Price = price,
                    Size = size,
                    VideoId = videoId,
                    ReleaseDate = releaseDate
                };

                db.Add(game);
                db.SaveChanges();
            }

            return true;
        }

        public IEnumerable<AdminListGamesViewModel> AllGames()
        {
            using (var db = new GameStoreContext())
            {
                return db
                    .Games
                    .Select(g => new AdminListGamesViewModel
                    {
                        Id = g.Id,
                        Name = g.Title,
                        Size = g.Size,
                        Price = g.Price
                    })
                    .ToList();
            }
        }

        public Game GetGame(int id)
        {
            using (var db = new GameStoreContext())
            {
                var game = db
                    .Games
                    .FirstOrDefault(g => g.Id == id);

                return game;
            }
        }

        public void Edit(
            int id,
            string title,
            string description,
            string image, 
            decimal price,
            double size, 
            string videoId, 
            DateTime releaseDate)
        {
            using (var db = new GameStoreContext())
            {
                var game = db
                    .Games
                    .FirstOrDefault(g => g.Id == id);

                game.Title = title;
                game.Description = description;
                game.Image = image;
                game.Price = price;
                game.Size = size;
                game.VideoId = videoId;
                game.ReleaseDate = releaseDate;

                db.SaveChanges();
            }
        }
    }
}