﻿namespace BookLibrary.Models
{
    using System.Collections.Generic;

    public class Borrower
    {
        public Borrower()
        {
            this.BorrowedBooks = new List<BorrowersBooks>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<BorrowersBooks> BorrowedBooks { get; set; }
    }
}