namespace WebServer.ByTheCakeApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using ViewModels;

    public class ByTheCakeApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
                "/", 
                req => new HomeController().Index());

            appRouteConfig.Get(
                "/about",
                req => new HomeController().About());

            appRouteConfig.Get(
                "/add",
                req => new CakesController().Add());

            appRouteConfig.Post(
                "/add",
                req => new CakesController().Add(
                    req.FormData["name"], req.FormData["price"]));

            appRouteConfig.Get(
                "/search",
                req => new CakesController().Search(req.UrlParameters));


            //appRouteConfig.Get(
            //    "/",
            //    req => new HomeController().Home());

            //appRouteConfig.Get(
            //    "/login",
            //    req => new AccountController().Login());

            //appRouteConfig.Post(
            //    "/login",
            //    req => new AccountController().Login(req, new LoginViewModel
            //    {
            //        Username = req.FormData["username"],
            //        Password = req.FormData["password"]
            //    }));
        }
    }
}