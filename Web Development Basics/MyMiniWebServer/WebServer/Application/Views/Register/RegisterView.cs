namespace WebServer.Application.Views.Register
{
    using Server.Contracts;
    using System.Text;

    public class RegisterView : IView
    {
        public string View()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<body>");
            sb.AppendLine(new string(' ', 3) + "<form method=\"POST\">");
            sb.AppendLine(new string(' ', 7) + "Name</br>");
            sb.AppendLine(new string(' ', 7) + "<input type=\"text\" name=\"name\" /><br/>");
            sb.AppendLine(new string(' ', 7) + "<input type=\"submit\"/>");
            sb.AppendLine(new string(' ', 3) + "</form>");
            sb.AppendLine("</body>");

            return sb.ToString().Trim();
        }
    }
}