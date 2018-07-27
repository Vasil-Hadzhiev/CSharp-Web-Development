namespace Library.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class MovieStatusViewModel
    {
        public MovieStatusViewModel()
        {
            this.MovieRecords = new List<MovieRecordsViewModel>();
        }

        public string Title { get; set; }

        public List<MovieRecordsViewModel> MovieRecords { get; set; }
    }
}