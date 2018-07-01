namespace Chushka.App.Controllers
{
    using Chushka.Models;
    using Models.BindingModels;
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Interfaces;
    using System;
    using System.Linq;
    using System.Text;

    public class ProductsController : BaseController
    {
        [HttpGet]
        public IActionResult Create()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (this.User.Roles.First() != "Admin")
            {
                return this.RedirectToHome();
            }

            var productTypes = this.Context
                .ProductTypes;

            var result = new StringBuilder();

            foreach (var pt in productTypes)
            {
                result.Append($@"<label class=""radio - inline""><input type=""radio"" name=""ProductTypeId"" value=""{pt.Id}"">{pt.Name}</label>");
                result.Append(" ");
            }

            this.ViewData.Data["productTypes"] = result.ToString();

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ProductBindingModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (this.User.Roles.First() != "Admin")
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid product details.");
                return this.View();
            }

            var name = model.Name;
            var price = model.Price;
            var description = model.Description;
            var productType = this.Context.ProductTypes.FirstOrDefault(pt => pt.Id == model.ProductTypeId);

            var product = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                TypeId = productType.Id
            };

            this.Context.Products.Add(product);
            this.Context.SaveChanges();

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var product = this.Context
                .Products
                .FirstOrDefault(p => p.Id == id);

            var name = product.Name;
            var price = product.Price;
            var description = product.Description;
            var productType = this.Context
                .ProductTypes
                .FirstOrDefault(pt => pt.Id == product.TypeId)
                .Name;

            this.ViewData.Data["name"] = name;
            this.ViewData.Data["price"] = price.ToString();
            this.ViewData.Data["description"] = description;
            this.ViewData.Data["productType"] = productType;
            this.ViewData.Data["productId"] = product.Id.ToString();

            var result = new StringBuilder();

            if (this.User.Roles.First() == "Admin")
            {
                result.Append($@"<a class=""btn chushka-bg-color"" href=""/products/edit?id={product.Id}"">Edit</a>");
                result.Append($@"<a class=""btn chushka-bg-color"" href=""/products/delete?id={product.Id}"">Delete</a>");

                this.ViewData.Data["adminButtons"] = result.ToString();
            }
            else
            {
                this.ViewData.Data["adminButtons"] = string.Empty;
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

            if (this.User.Roles.First() != "Admin")
            {
                return this.RedirectToHome();
            }

            var product = this.Context
                .Products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return this.RedirectToHome();
            }

            var productTypes = this.Context
                .ProductTypes;

            var result = new StringBuilder();

            foreach (var pt in productTypes)
            {
                if (pt.Id == product.TypeId)
                {
                    result.Append($@"<label class=""radio-inline active""><input type=""radio"" name=""ProductTypeId"" value=""{pt.Id}"" checked="""">{pt.Name}</label>");
                }
                else
                {
                    result.Append($@"<label class=""radio-inline""><input type=""radio"" name=""ProductTypeId"" value=""{pt.Id}"">{pt.Name}</label>");
                }
                
                result.Append(" ");
            }

            this.ViewData.Data["name"] = product.Name;
            this.ViewData.Data["price"] = product.Price.ToString();
            this.ViewData.Data["description"] = product.Description;
            this.ViewData.Data["productTypes"] = result.ToString();

            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductBindingModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (this.User.Roles.First() != "Admin")
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError("Invalid product details.");
                return this.View();
            }

            var product = this.Context
                .Products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return this.RedirectToHome();
            }

            var productType = this.Context.ProductTypes.FirstOrDefault(pt => pt.Id == model.ProductTypeId);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.TypeId = productType.Id;

            this.Context.SaveChanges();

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (this.User.Roles.First() != "Admin")
            {
                return this.RedirectToHome();
            }

            var product = this.Context
                .Products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return this.RedirectToHome();
            }

            this.ViewData.Data["id"] = product.Id.ToString();
            this.ViewData.Data["name"] = product.Name;
            this.ViewData.Data["price"] = product.Price.ToString();
            this.ViewData.Data["description"] = product.Description;

            var productTypes = this.Context
                .ProductTypes;

            var result = new StringBuilder();

            foreach (var pt in productTypes)
            {
                if (pt.Id == product.TypeId)
                {
                    result.Append($@"<label class=""radio-inline active""><input disabled type=""radio"" name=""ProductTypeId"" value=""{pt.Id}"" checked="""">{pt.Name}</label>");
                }
                else
                {
                    result.Append($@"<label class=""radio-inline""><input disabled type=""radio"" name=""ProductTypeId"" value=""{pt.Id}"">{pt.Name}</label>");
                }

                result.Append(" ");
            }

            this.ViewData.Data["productTypes"] = result.ToString();

            return this.View();
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (this.User.Roles.First() != "Admin")
            {
                return this.RedirectToHome();
            }

            var product = this.Context
                .Products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return this.RedirectToHome();
            }
           
            this.Context.Products.Remove(product);
            this.Context.SaveChanges();

            return this.RedirectToHome();
        }

        [HttpPost]
        public IActionResult Order(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var product = this.Context
                .Products
                .FirstOrDefault(p => p.Id == id);

            var user = this.Context
                .Users
                .FirstOrDefault(u => u.Username == this.User.Name);

            var orderedOn = DateTime.UtcNow;
            var orderId = Guid.NewGuid().ToString();

            var order = new Order
            {
                OrderId = orderId,
                ClientId = user.Id,
                ProductId = product.Id,
                OrderedOn = orderedOn
            };

            this.Context.Orders.Add(order);
            this.Context.SaveChanges();

            return this.RedirectToAction("/orders/all");
        }
    }
}