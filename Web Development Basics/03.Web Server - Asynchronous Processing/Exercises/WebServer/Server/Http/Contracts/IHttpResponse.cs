namespace TestWebServer.Server.Http.Contracts
{
    using Enums;

    public interface IHttpResponse
    {
        IHttpHeaderCollection Headers { get; }

        HttpStatusCode StatusCode { get; }
    }
}