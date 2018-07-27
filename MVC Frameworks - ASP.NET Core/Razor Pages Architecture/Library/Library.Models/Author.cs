namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        public Author()
        {
            this.Books = new List<Book>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}