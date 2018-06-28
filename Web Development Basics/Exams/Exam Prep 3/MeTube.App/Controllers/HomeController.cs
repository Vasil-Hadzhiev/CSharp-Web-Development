namespace MeTube.App.Controllers
{
    using Services;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Text;

    public class HomeController : BaseController
    {
        private readonly ITubeService tubes;

        public HomeController()
        {
            this.tubes = new TubeService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model.Data["username"] = this.User.Name;
                this.Model.Data["anonymousDisplay"] = "none";
                this.Model.Data["authDisplay"] = "block";

                var tubes = this.tubes.All();

                var tubesResult = new StringBuilder();
                tubesResult.Append(@"<div class=""row text-center"">");
                for (int i = 0; i < tubes.Count; i++)
                {
                    var tube = tubes[i];
                    tubesResult.Append(
                        $@"<div class=""col-4"">
                            <img class=""img-thumbnail tube-thumbnail"" src=""https://img.youtube.com/vi/{tube.YouTubeId}/0.jpg"" alt=""{tube.Title}"" />
                            <div>
                                <h5>{tube.Title}</h5>
                                <h5>{tube.Author}</h5>
                            </div>
                        </div>");

                    if (i % 3 == 2)
                    {
                        tubesResult.Append(@"</div><div class=""row text-center"">");
                    }
                }

                tubesResult.Append("</div>");

                this.Model.Data["tubes"] = tubesResult.ToString();
            }
            else
            {
                this.Model.Data["anonymousDisplay"] = "block";
                this.Model.Data["authDisplay"] = "none";         
            }

            return this.View();
        }
    }
}