namespace ModPanel.App.Controllers
{
    using Models.ViewModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;
    using System.Text;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                this.Model.Data["guestDisplay"] = "block";
                this.Model.Data["adminDisplay"] = "none";
                this.Model.Data["userDisplay"] = "none";
            }
            else
            {
                this.Model.Data["guestDisplay"] = "none";
                this.Model.Data["userDisplay"] = "block";
                this.Model.Data["adminDisplay"] = "none";

                using (this.Context)
                {
                    var posts = this.Context
                        .Posts
                        .OrderByDescending(p => p.Id)
                        .Select(p => new PostIndexViewModel
                        {
                            Title = p.Title,
                            Content = p.Content,
                            Date = p.CreatedOn.ToString("dd'.'MM'.'yyyy"),
                            Author = p.User.Email
                        })
                        .ToList();

                    var postsResult = posts
                        .Select(p => $@"
                            <div class=""card border-primary mb-3"">
                                <div class=""card-body text-primary"">
                                    <h4 class=""card-title"">{p.Title}</h4>
                                    <p class=""card-text"">
                                        {p.Content}
                                    </p>
                                </div>
                                <div class=""card-footer bg-transparent text-right"">
                                    <span class=""text-muted"">
                                        Created on {p.Date} by
                                        <em>
                                            <strong>{p.Author}</strong>
                                        </em>
                                    </span>
                                </div>
                            </div>");

                    this.Model.Data["posts"] = postsResult.Any()
                    ? string.Join(string.Empty, postsResult)
                    : "<h2>No posts found!</h2>";

                    if (this.CurrentUser.IsAdmin)
                    {
                        this.Model.Data["adminDisplay"] = "block";
                        this.Model.Data["userDisplay"] = "none";

                        var logs = this.Context
                                .Logs
                                .OrderByDescending(l => l.Id)
                                .ToList();

                        var result = new StringBuilder();

                        var color = string.Empty;

                        for (int i = 0; i < logs.Count; i++)
                        {
                            var log = logs[i];

                            if (log.Activity.Contains("opened"))
                            {
                                color = "info";
                            }
                            else if (log.Activity.Contains("deleted"))
                            {
                                color = "danger";
                            }
                            else if (log.Activity.Contains("approved"))
                            {
                                color = "success";
                            }
                            else if (log.Activity.Contains("edited"))
                            {
                                color = "warning";
                            }

                            result.Append($@"<div class=""card border-{color} mb-1"">");
                            result.Append($@"<div class=""card-body"">");
                            result.Append($@"<p class=""card-text"">{log.Activity}</p>");
                            result.Append("</div>");
                            result.Append("</div>");
                        }

                        this.Model.Data["logs"] = result.ToString();
                    }
                }
            }

            return this.View();
        }
    }
}