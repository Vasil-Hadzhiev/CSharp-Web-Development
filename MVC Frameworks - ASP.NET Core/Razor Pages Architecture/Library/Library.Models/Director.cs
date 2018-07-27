namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Director
    {
        public Director()
        {
            this.Movies = new List<Movie>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } 
    }
}