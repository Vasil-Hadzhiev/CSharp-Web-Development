namespace Library.Models
{
    using System;
    using System.Collections.Generic;

    public class Record
    {
        public Record()
        {
            this.BookRecords = new List<BooksRecords>();
            this.MovieRecords = new List<MoviesRecords>();
        }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }      

        public DateTime? EndDate { get; set; }

        public ICollection<BooksRecords> BookRecords { get; set; } 

        public ICollection<MoviesRecords> MovieRecords { get; set; }
    }
}