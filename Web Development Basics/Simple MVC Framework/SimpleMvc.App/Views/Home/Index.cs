namespace SimpleMvc.App.Views.Home
{
    using SimpleMvc.Framework.Interfaces;
    using System.Text;

    public class Index : IRenderable
    {
        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<h3>NotesApp</h3>");
            sb.AppendLine(@"<a href=""/user/all"">All Users</a>");
            sb.AppendLine(@"<a href=""/user/register"">Register Users</a>");

            return sb.ToString();
        }
    }
}