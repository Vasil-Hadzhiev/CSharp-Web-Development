namespace WebServer.Server.Http.Response
{
    using Common;
    using Enums;

    public class RedirectResponse : HttpResponse
    {
        protected RedirectResponse(string redirectUrl) 
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpStatusCode.Found;
            this.Headers.Add(new HttpHeader("Location", redirectUrl));
        }
    }
}