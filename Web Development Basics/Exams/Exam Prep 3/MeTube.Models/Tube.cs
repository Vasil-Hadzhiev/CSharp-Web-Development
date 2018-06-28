namespace MeTube.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tube
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string YoutubeId { get; set; }

        public int Views { get; set; } = 0;

        public User Uploader { get; set; }

        public int UploaderId { get; set; }
    }
}