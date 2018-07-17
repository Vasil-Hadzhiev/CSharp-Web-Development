namespace BookLibrary.Models
{
    using System.Collections.Generic;

    public class Book
    {
        public Book()
        {
            this.Borrowers = new List<BorrowersBooks>();
            this.Records = new List<BookRecord>();        
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CoverImage { get; set; }

        public string Status { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<BorrowersBooks> Borrowers { get; set; }
        
        public ICollection<BookRecord> Records { get; set; }
    }
}