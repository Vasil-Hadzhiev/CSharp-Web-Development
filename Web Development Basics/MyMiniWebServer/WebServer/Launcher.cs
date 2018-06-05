namespace WebServer
{
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
            var application = new ByTheCakeApp();
            var appRouteConfig = new AppRouteConfig();
            application.Configure(appRouteConfig);

            var webServer = new WebServer(1337, appRouteConfig);

            webServer.Run();
        }
    }
}