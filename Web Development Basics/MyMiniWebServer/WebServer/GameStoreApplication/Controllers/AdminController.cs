namespace WebServer.GameStoreApplication.Controllers
{
    using Server.Http.Contracts;
    using Services;
    using Services.Contracts;
    using System;
    using System.Globalization;
    using System.Text;
    using ViewModels.Admin;

    public class AdminController : BaseController
    {
        private const string ListGamesView = @"admin\admin-games";
        private const string AddGameView = @"admin\add-game";
        private const string EditGameView = @"admin\edit-game";

        private readonly IGameService games;

        public AdminController(IHttpRequest request) 
            : base(request)
        {
            this.games = new GameService();
        }

        public IHttpResponse ListGames()
        {
            var isAdmin = this.Authentication.IsAdmin;
            if (!isAdmin)
            {
                return this.RedirectResponse("/");
            }

            var games = this.games.AllGames();

            //var allGames = games
            //    .Select(g =>
            //    $"<tr>" +
            //        $"<td>{g.Id}</td>" +
            //        $"<td>{g.Name}</td>" +
            //        $"<td>{g.Size:f2} GB</td>" +
            //        $"<td>{g.Price:f2} &euro;</td>" +
            //        $"<td>" +
            //            $@"<a class=""btn btn-warning"" href=""/admin/games/edit/{g.Id}"">Edit</a>" +
            //            $@"<a class=""btn btn-danger"" href=""/admin/games/delete/{g.Id}"">Delete</a>" +
            //        $"</td>" +
            //    $"</tr>");

            //var gamesAsString = string.Join(Environment.NewLine, allGames);

            //this.ViewData["games"] = gamesAsString;

            var result = new StringBuilder();

            var count = 1;

            foreach (var game in games)
            {
                var id = $"<td>{game.Id}</td>";
                var name = $"<td>{game.Name}</td>";
                var size = $"<td>{game.Size:f2} GB</td>";
                var price = $"<td>{game.Price:f2} &euro;</td>";
                var editBtn = $@"<a class=""btn btn-warning"" href=""/admin/games/edit/{game.Id}"">Edit</a>";
                var deleteBtn = $@"<a class=""btn btn-danger"" href=""/admin/games/delete/{game.Id}"">Delete</a>";

                if (count % 2 != 0)
                {
                    result.AppendLine(@"<tr class=""table-warning"">");
                }
                else
                {
                    result.AppendLine("<tr>");
                }

                result.AppendLine(id);
                result.AppendLine(name);
                result.AppendLine(size);
                result.AppendLine(price);
                result.AppendLine("<td>");
                result.AppendLine(editBtn);
                result.AppendLine(deleteBtn);
                result.AppendLine("</td>");
                result.AppendLine("</tr>");

                count++;
            }

            this.ViewData["games"] = result.ToString();

            return this.FileViewResponse(ListGamesView);
        }

        public IHttpResponse AddGame()
        {
            var isAdmin = this.Authentication.IsAdmin;
            if (!isAdmin)
            {
                return this.RedirectResponse("/");
            }

            return this.FileViewResponse(AddGameView);
        }

        public IHttpResponse AddGame(AddGameViewModel model)
        {
            var isAdmin = this.Authentication.IsAdmin;
            if (!isAdmin)
            {
                return this.RedirectResponse("/");
            }

            var isValid = this.ValidateModel(model);

            if (!isValid)
            {
                return this.FileViewResponse(AddGameView);
            }

            var title = model.Title;
            var description = model.Description;
            var image = model.Image;
            var price = model.Price;
            var size = model.Size;
            var videoId = model.VideoId;
            var releaseDate = model.ReleaseDate;

            var success = this.games.Create(title, description, image, price, size, videoId, releaseDate.Value);

            if (success)
            {
                return this.RedirectResponse(@"/admin/games/list");
            }
            else
            {
                this.ShowError("Game already exists.");

                return this.FileViewResponse(AddGameView);
            }
        }

        public IHttpResponse Edit(int id)
        {
            var game = this.games.GetGame(id);

            var title = game.Title;
            var description = game.Description;
            var image = game.Image;
            var price = game.Price;
            var size = game.Size;
            var videoId = game.VideoId;
            var releaseDate = game.ReleaseDate;

            this.ViewData["title"] = title;
            this.ViewData["description"] = description;
            this.ViewData["image"] = image;
            this.ViewData["price"] = price.ToString();
            this.ViewData["size"] = size.ToString();
            this.ViewData["videoId"] = videoId;
            this.ViewData["releaseDate"] = releaseDate.ToString("yyyy-MM-dd");

            return this.FileViewResponse(EditGameView);
        }

        public IHttpResponse Edit(EditGameView model)
        {
            var id = model.Id;
            var title = model.Title;
            var description = model.Description;
            var image = model.Image;
            var price = model.Price;
            var size = model.Size;
            var videoId = model.VideoId;
            var releaseDate = model.ReleaseDate;

            this.games.Edit(id, title, description, image, price, size, videoId, releaseDate.Value);

            return this.FileViewResponse(ListGamesView);
        }
    }
}