namespace MeTube.App.Controllers
{
    using Models;
    using Services;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;

    public class TubeController : BaseController
    {
        private readonly ITubeService tube;

        public TubeController()
        {
            this.tube = new TubeService();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Upload(UploadTubeViewModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid tube details.");
                return this.View();
            }

            var title = model.Title;
            var author = model.Author;
            var youtubeId = this.GetYouTubeIdFromLink(model.YoutubeLink);
            var description = model.Description;
            var userId = this.CurrentUser.Id;

            if (youtubeId == null)
            {
                this.ShowError("Invalid youtube link.");
                return this.View();
            }

            var success = this.tube.Upload(title, author, youtubeId, description, userId);
            if (!success)
            {
                this.ShowError("Tube already exists.");
                return this.View();
            }

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            using (this.Context)
            {
                var tube = this.Context.Tubes.Find(id);
                if (tube == null)
                {
                    return this.RedirectToHome();
                }

                tube.Views++;
                this.Context.SaveChanges();

                this.Model.Data["title"] = tube.Title;
                this.Model.Data["youtubeId"] = tube.YoutubeId;
                this.Model.Data["author"] = tube.Author;
                this.Model.Data["views"] = $"{tube.Views} View{(tube.Views == 1 ? "" : "s")}";
                this.Model.Data["description"] = tube.Description;

                return this.View();
            }
        }

        private string GetYouTubeIdFromLink(string youTubeLink)
        {
            string youTubeId = null;
            if (youTubeLink.Contains("youtube.com"))
            {
                youTubeId = youTubeLink.Split("?v=")[1];
            }
            else if (youTubeLink.Contains("youtu.be"))
            {
                youTubeId = youTubeLink.Split("/").Last();
            }

            return youTubeId;
        }
    }
}