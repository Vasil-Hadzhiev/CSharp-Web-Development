namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        public Movie()
        {
            this.Records = new List<MoviesRecords>();
            this.Borrowers = new List<BorrowersMovies>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Status { get; set; }

        public string CoverImg { get; set; }

        public int DirectorId { get; set; }

        public Director Director { get; set; }

        public ICollection<MoviesRecords> Records { get; set; } 

        public ICollection<BorrowersMovies> Borrowers { get; set; } 
    }
}