namespace Library.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class DirectorsDetailsViewModel
    {
        public DirectorsDetailsViewModel()
        {
            this.Movies = new List<DirectorsMoviesViewModel>();
        }

        public string Director { get; set; }

        public List<DirectorsMoviesViewModel> Movies { get; set; }
    }
}