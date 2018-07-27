namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Book
    {
        public Book()
        {
            this.Records = new List<BooksRecords>();
            this.Borrowers = new List<BorrowersBooks>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string CoverImg { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<BooksRecords> Records { get; set; }

        public ICollection<BorrowersBooks> Borrowers { get; set; }
    }
}