namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using System;
    using System.Linq;
    using ViewModels;
    using ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductService products;

        public ProductsController()
        {
            this.products = new ProductService();
        }

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"products\add");
        }

        public IHttpResponse Add(AddProductViewModel model)
        {
            var name = model.Name;
            var price = model.Price;
            var imageUrl = model.ImageUrl;

            if (name.Length < 3
               || name.Length > 30
               || imageUrl.Length < 3
               || imageUrl.Length > 2000)
            {
                this.AddError("Product information is not valid");

                return this.FileViewResponse(@"products\add");
            }

            this.products.Create(name, price, imageUrl);

            this.ViewData["name"] = name;
            this.ViewData["price"] = price.ToString();
            this.ViewData["imageUrl"] = imageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"products\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;

            var searchTerm = urlParameters.ContainsKey(searchTermKey)
                ? urlParameters[searchTermKey]
                : null;

            this.ViewData["searchTerm"] = searchTerm;

            var result = this.products.All(searchTerm);

            if (!result.Any())
            {
                this.ViewData["results"] = "No cakes found";
            }
            else
            {
                var allProducts = result
                    .Select(c => $@"<div><a href=""/cakes/{c.Id}"">{c.Name}</a> - ${c.Price:F2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>");

                var allProductsAsString = string.Join(Environment.NewLine, allProducts);

                this.ViewData["results"] = allProductsAsString;
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.ProductIds.Any())
            {
                var totalProducts = shoppingCart.ProductIds.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"products\search");
        }

        public IHttpResponse Details(int id)
        {
            var product = this.products.Find(id);

            if (product == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString("F2");
            this.ViewData["imageUrl"] = product.ImageUrl;

            return this.FileViewResponse(@"products\details");
        }
    }
}