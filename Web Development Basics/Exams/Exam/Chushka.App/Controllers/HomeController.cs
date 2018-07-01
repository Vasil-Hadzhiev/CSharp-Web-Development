namespace Chushka.App.Controllers
{
    using Models.ViewModels;
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Interfaces;
    using System.Linq;
    using System.Text;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                this.ViewData.Data["guestDisplay"] = "block";
                this.ViewData.Data["authDisplay"] = "none";
            }
            else
            {
                this.ViewData.Data["guestDisplay"] = "none";
                this.ViewData.Data["authDisplay"] = "block";

                var products = this.Context
                    .Products
                    .Select(p => new ProductsIndexViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    })
                    .ToList();

                var result = new StringBuilder();

                for (int i = 0; i < products.Count; i++)
                {
                    var product = products[i];

                    if (i == 0)
                    {
                        result.Append($@"<div class=""row d-flex justify-content-around"">");
                    }
                    else if (i % 5 == 0)
                    {
                        result.Append($@"<div class=""row d-flex justify-content-around mt-4"">");
                    }

                    var description = product.Description;
                    if (description.Length > 50)
                    {
                        description = product.Description.Substring(0, 50) + "...";
                    }
                    
                    result.Append($@"<a href=""/products/details?id={product.Id}"" class=""col-md-2"">");
                    result.Append($@"<div class=""product p-1 chushka-bg-color rounded-top rounded-bottom"">");
                    result.Append($@"<h5 class=""text-center mt-3"">{product.Name}</h5>");
                    result.Append($@"<hr class=""hr-1 bg-white""/>");
                    result.Append($@"<p class=""text-white text-center"">");
                    result.Append($@"{description}");
                    result.Append($"</p>");
                    result.Append($@"<hr class=""hr-1 bg-white""/>");
                    result.Append($@"<h6 class=""text-center text-white mb-3"">{product.Price:f2}</h6>");
                    result.Append($@"</div>");
                    result.Append($@"</a>");

                    if ((i + 1) % 5 == 0)
                    {
                        result.Append($@"</div>");
                    }
                }

                this.ViewData.Data["username"] = this.User.Name;
                this.ViewData.Data["products"] = result.ToString();
            }

            return this.View();
        }
    }
}