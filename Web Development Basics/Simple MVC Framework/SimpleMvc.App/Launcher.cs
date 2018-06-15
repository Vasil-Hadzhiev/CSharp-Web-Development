namespace SimpleMvc.App
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main()
        {
            using (var db = new NotesDbContext())
            {
                db.Database.Migrate();
            }

            var server = new WebServer(8000, new ControllerRouter());

            MvcEngine.Run(server);
        }
    }
}