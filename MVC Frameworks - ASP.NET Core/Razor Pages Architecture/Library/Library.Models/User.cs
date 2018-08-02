namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User 
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}