namespace ModPanel.App.Controllers
{
    using Models.BindingModels;
    using ModPanel.Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System;
    using System.Linq;

    public class PostsController : BaseController
    {
        [HttpGet]
        public IActionResult Create()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(PostBindingModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid post details.");
                return this.View();
            }

            var title = model.Title;
            var content = model.Content;

            if (this.Context.Posts.FirstOrDefault(p => p.Title == title) != null)
            {
                this.ShowError("Post already exists.");
                return this.View();
            }

            var post = new Post
            {
                Title = title,
                Content = content,
                CreatedOn = DateTime.UtcNow,
                UserId = this.CurrentUser.Id
            };

            using (this.Context)
            {
                this.Context.Posts.Add(post);
                this.Context.SaveChanges();
            }

            return this.RedirectToHome();
        }
    }
}