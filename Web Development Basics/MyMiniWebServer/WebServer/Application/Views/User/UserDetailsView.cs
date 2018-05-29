namespace WebServer.Application.Views.User
{
    using Server;
    using Server.Contracts;
    using System;

    public class UserDetailsView : IView
    {
        private readonly Model model;

        public UserDetailsView(Model model)
        {
            this.model = model;
        }

        public string View()
        {
            return $"<body>Hello, {this.model["name"]}!</body>" + Environment.NewLine;
        }
    }
}