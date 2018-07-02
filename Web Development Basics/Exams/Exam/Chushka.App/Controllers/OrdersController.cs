namespace Chushka.App.Controllers
{
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Interfaces;
    using System.Linq;
    using System.Text;

    public class OrdersController : BaseController
    {
        [HttpGet]
        public IActionResult All()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToHome();
            }

            var orders = this.Context
                .Orders
                .ToList();

            var result = new StringBuilder();

            foreach (var order in orders)
            {
                var customer = this.Context
                    .Users
                    .FirstOrDefault(u => u.Id == order.ClientId);

                var product = this.Context
                    .Products
                    .FirstOrDefault(p => p.Id == order.ProductId);

                result.Append("<tr>");
                result.Append($@"<td>{order.Id}</td>");
                result.Append($@"<td>{order.OrderId}</td>");
                result.Append($@"<td>{customer.Username}</td>");
                result.Append($@"<td>{product.Name}</td>");
                result.Append($@"<td>{order.OrderedOn.ToShortDateString()}");
                result.Append("</tr>");
            }

            this.ViewData.Data["orders"] = result.ToString();

            return this.View();
        }
    }
}