namespace MeTube.App.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UploadTubeViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string YoutubeLink { get; set; }

        public string Description { get; set; }
    }
}