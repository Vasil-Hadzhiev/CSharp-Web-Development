namespace SimpleMvc.Framework.ActionResults
{
    using Contracts;

    public class RedirectResult : IRedirectable
    {
        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; set; }

        public string Invoke()
        {
            return this.RedirectUrl;
        }
    }
}