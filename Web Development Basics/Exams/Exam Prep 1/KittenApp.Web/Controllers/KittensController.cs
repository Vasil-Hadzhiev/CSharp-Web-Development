namespace KittenApp.Web.Controllers
{
    using Data;
    using KittenApp.Models;
    using Models;
    using Services;
    using Services.Contracts;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Linq;
    using System.Text;

    public class KittensController : BaseController
    {
        private const int KittensPerPage = 3;

        private readonly IKittensService kittens;

        public KittensController()
        {
            this.kittens = new KittensService();

            using (var db = new KittenAppContext())
            {
                if (!db.Breeds.Any())
                {
                    db.Breeds.Add(new Breed { Name = "Street Transcended" });
                    db.Breeds.Add(new Breed { Name = "American Shorthair" });
                    db.Breeds.Add(new Breed { Name = "Munchkin" });
                    db.Breeds.Add(new Breed { Name = "Siamese" });

                    db.SaveChanges();
                }
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddKittenViewModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid kitten details.");
                return this.View();
            }

            var name = model.Name;
            var age = model.Age;
            var breed = model.Breed;

            var success = this.kittens.Add(name, age, breed);
            if (!success)
            {
                this.ShowError("Invalid breed.");
                return this.View();
            }

            return this.RedirectToAction("/kittens/all");
        }

        [HttpGet]
        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var kittens = this.kittens.All();

            var kittensResult = new StringBuilder();
            kittensResult.Append(@"<div class=""row text-center"">");
            for (int i = 0; i < kittens.Count; i++)
            {
                kittensResult.Append(kittens[i]);

                if (i % KittensPerPage == KittensPerPage - 1)
                {
                    kittensResult.Append(@"</div><div class=""row text-center"">");
                }
            }

            kittensResult.Append("</div>");

            this.Model.Data["kittens"] = kittensResult.ToString();
            return this.View();
        }
    }
}