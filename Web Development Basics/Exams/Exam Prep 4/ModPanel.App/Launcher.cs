namespace ModPanel.App
{
    using Data;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            var server = new WebServer(
                1337,
                new ControllerRouter(),
                new ResourceRouter());
            var dbContext = new ModPanelContext();

            MvcEngine.Run(server, dbContext);
        }
    }
}