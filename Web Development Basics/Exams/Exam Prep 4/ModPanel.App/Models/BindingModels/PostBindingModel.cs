namespace ModPanel.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class PostBindingModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}