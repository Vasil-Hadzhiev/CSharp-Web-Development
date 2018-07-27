namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Borrower
    {
        public Borrower()
        {
            this.BorrowedBooks = new List<BorrowersBooks>();
            this.BorrowedMovies = new List<BorrowersMovies>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        public ICollection<BorrowersBooks> BorrowedBooks { get; set; } 

        public ICollection<BorrowersMovies> BorrowedMovies { get; set; }
    }
}
