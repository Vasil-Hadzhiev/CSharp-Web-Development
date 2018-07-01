namespace ModPanel.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Posts = new List<Post>();
        }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Position { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsApproved { get; set; }

        public List<Post> Posts { get; set; }
    }
}