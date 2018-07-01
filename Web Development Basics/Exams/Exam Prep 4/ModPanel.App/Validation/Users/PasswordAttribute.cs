namespace ModPanel.App.Validation.Users
{
    using SimpleMvc.Framework.Attributes.Property;
    using System.Linq;

    public class PasswordAttribute : PropertyAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;
            if (password == null)
            {
                return true;
            }

            return password.Any(s => char.IsDigit(s))
                && password.Any(s => char.IsUpper(s))
                && password.Any(s => char.IsLower(s))
                && password.Length >= 6;
        }
    }
}