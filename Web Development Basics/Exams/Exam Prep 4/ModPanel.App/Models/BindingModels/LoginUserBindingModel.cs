namespace ModPanel.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserBindingModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}