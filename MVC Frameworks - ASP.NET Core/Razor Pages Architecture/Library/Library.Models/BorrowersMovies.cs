namespace Library.Models
{
    public class BorrowersMovies
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
    }
}