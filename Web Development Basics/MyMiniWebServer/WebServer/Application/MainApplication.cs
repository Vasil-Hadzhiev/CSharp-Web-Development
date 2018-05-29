﻿namespace WebServer.Application
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
                "/",
                req => new HomeController().Index());

            appRouteConfig.Get(
                "/testsession",
                req => new HomeController().SessionTest(req));

            appRouteConfig.Get(
                "/users/{(?<name>[a-z]+)}",
                req => new HomeController().Index());

            appRouteConfig.Get(
                "/register",
                req => new UserController().RegisterGet());

            appRouteConfig.Post(
                "/register",
                req => new UserController().RegisterPost(req.FormData["name"]));

            appRouteConfig.Get(
                "/user/{(?<name>[a-zA-Z]+)}",
                req => new UserController().Details(req.UrlParameters["name"]));
        }
    }
}