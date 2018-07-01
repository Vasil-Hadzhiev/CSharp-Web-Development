namespace ModPanel.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using Validation.Users;

    public class RegisterUserBindingModel
    {
        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        [Password]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Position { get; set; }
    }
}