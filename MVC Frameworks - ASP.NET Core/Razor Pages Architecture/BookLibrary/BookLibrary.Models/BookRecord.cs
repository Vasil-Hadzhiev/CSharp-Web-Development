namespace BookLibrary.Models
{
    using System;

    public class BookRecord
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}