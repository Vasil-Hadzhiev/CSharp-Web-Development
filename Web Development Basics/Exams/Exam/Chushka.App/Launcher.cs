namespace Chushka.App
{
    using Chushka.Models;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using SoftUni.WebServer.Mvc;
    using SoftUni.WebServer.Mvc.Routers;
    using SoftUni.WebServer.Server;
    using System.Linq;

    public class Launcher
    {
        public static void Main()
        {
            var server = new WebServer(
                1337,
                new ControllerRouter(),
                new ResourceRouter());

            using (var db = new ChushkaContext())
            {
                if (!db.ProductTypes.Any())
                {
                    db.ProductTypes.Add(new ProductType { Name = "Food" });
                    db.ProductTypes.Add(new ProductType { Name = "Domestic" });
                    db.ProductTypes.Add(new ProductType { Name = "Health" });
                    db.ProductTypes.Add(new ProductType { Name = "Cosmetic" });
                    db.ProductTypes.Add(new ProductType { Name = "Other" });
                    db.SaveChanges();
                }

                db.Database.Migrate();
            }

            MvcEngine.Run(server);
        }
    }
}