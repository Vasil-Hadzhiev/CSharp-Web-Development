namespace WebServer
{
    using Application;
    using ByTheCakeApplication;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
    {
        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var byTheCakeApp = new ByTheCakeApp();
            //var mainApplication = new MainApplication();
            var appRouteConfig = new AppRouteConfig();
            byTheCakeApp.Configure(appRouteConfig);

            var webServer = new WebServer(1337, appRouteConfig);

            webServer.Run();
        }
    }
}