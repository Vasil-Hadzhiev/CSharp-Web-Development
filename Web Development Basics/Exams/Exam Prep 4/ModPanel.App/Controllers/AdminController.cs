namespace ModPanel.App.Controllers
{
    using Models.BindingModels;
    using Models.ViewModels;
    using ModPanel.Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;
    using System.Text;

    public class AdminController : BaseController
    {
        [HttpGet]
        public IActionResult Users()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();
            }

            var log = new Log
            {
                Admin = this.CurrentUser.Email,
                Activity = $"{this.CurrentUser.Email} opened Users Menu"
            };

            this.Context.Logs.Add(log);
            this.Context.SaveChanges();

            using (this.Context)
            {
                var users = this.Context
                    .Users
                    .Select(u => new UsersViewModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Position = u.Position,
                        Posts = u.Posts.Count,
                        IsApproved = u.IsApproved
                    })
                    .ToList();

                var result = new StringBuilder();

                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];

                    if (i % 2 == 0)
                    {
                        result.Append($@"<tr class=""table-primary"">");
                    }
                    else
                    {
                        result.Append($@"<tr class=""table-info"">");
                    }

                    result.Append($@"<td class=""text-center"">{user.Id}</td>");
                    result.Append($@"<td class=""text-center"">{user.Email}</td>");
                    result.Append($@"<td class=""text-center"">{user.Position}</td>");
                    result.Append($@"<td class=""text-center"">{user.Posts}</td>");

                    if (user.IsApproved)
                    {
                        result.Append($"<td>{string.Empty}</td>");
                    }
                    else
                    {
                        result.Append($@"<td><a class=""btn btn-primary btn-sm"" href=""/admin/approve?id={user.Id}"">Approve</a></td>");
                    }

                    result.Append("</tr>");
                }

                this.Model.Data["users"] = result.ToString();
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Approve(int id)
        {
            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();
            }

            using (this.Context)
            {
                var user = this.Context
                    .Users
                    .FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return this.View();
                }

                var log = new Log
                {
                    Admin = this.CurrentUser.Email,
                    Activity = $"{this.CurrentUser.Email} approved the registration of {user.Email}"
                };

                this.Context.Logs.Add(log);
                user.IsApproved = true;
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/admin/users");
        }

        [HttpGet]
        public IActionResult Posts()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();
            }

            using (this.Context)
            {
                var posts = this.Context
                    .Posts
                    .Select(p => new PostsViewModel
                    {
                        Id = p.Id,
                        Title = p.Title
                    })
                    .ToList();

                var result = new StringBuilder();

                for (int i = 0; i < posts.Count; i++)
                {
                    var post = posts[i];

                    if (i % 2 == 0)
                    {
                        result.Append($@"<tr class=""table-primary"">");
                    }
                    else
                    {
                        result.Append($@"<tr class=""table-info"">");
                    }

                    result.Append($@"<td>{post.Id}</td>");
                    result.Append($@"<td>{post.Title}</td>");
                    result.Append($@"<td>");
                    result.Append($@"<a class=""btn btn-warning btn-sm"" href=""/admin/edit?id={post.Id}"">Edit</a>");
                    result.Append(" ");
                    result.Append($@"<a class=""btn btn-danger btn-sm"" href=""/admin/delete?id={post.Id}"">Delete</a>");
                    result.Append($@"</td>");
                    result.Append("</tr>");
                }

                this.Model.Data["posts"] = result.ToString();

                var log = new Log
                {
                    Admin = this.CurrentUser.Email,
                    Activity = $"{this.CurrentUser.Email} opened Posts Menu"
                };

                this.Context.Logs.Add(log);
                this.Context.SaveChanges();
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();
            }

            var post = this.Context
                .Posts
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return this.RedirectToAction("/admin/posts");
            }

            this.Model.Data["title"] = post.Title;
            this.Model.Data["content"] = post.Content;

            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(int id, PostBindingModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();
            }

            using (this.Context)
            {
                var post = this.Context
                    .Posts
                    .FirstOrDefault(p => p.Id == id);

                if (post == null)
                {
                    return this.RedirectToAction("/admin/posts");
                }

                var log = new Log
                {
                    Admin = this.CurrentUser.Email,
                    Activity = $"{this.CurrentUser.Email} edited the posts \"{post.Title}\""
                };

                post.Title = model.Title;
                post.Content = model.Content;               

                this.Context.Logs.Add(log);
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/admin/posts");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();
            }

            var post = this.Context
                .Posts
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return this.RedirectToAction("/admin/posts");
            }

            this.Model.Data["id"] = id.ToString();
            this.Model.Data["title"] = post.Title;
            this.Model.Data["content"] = post.Content;

            return this.View();
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();
            }

            var post = this.Context
                .Posts
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return this.RedirectToAction("/admin/posts");
            }

            var log = new Log
            {
                Admin = this.CurrentUser.Email,
                Activity = $"{this.CurrentUser.Email} deleted the post \"{post.Title}\""
            };

            this.Context.Logs.Add(log);
            this.Context.Posts.Remove(post);
            this.Context.SaveChanges();

            return this.RedirectToAction("/admin/posts");
        }

        [HttpGet]
        public IActionResult Log()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.CurrentUser.IsAdmin)
            {
                return this.RedirectToHome();   
            }

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

            return this.View();
        }
    }
}