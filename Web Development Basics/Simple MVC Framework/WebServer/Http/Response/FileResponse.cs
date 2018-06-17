namespace WebServer.Http.Response
{
    using Contracts;
    using Enums;
    using Exceptions;

    public class FileResponse : IHttpResponse
    {
        public FileResponse(HttpStatusCode statusCode, byte[] fileData)
        {
            this.ValidateStatusCode(statusCode);

            this.FileData = fileData;
            this.StatusCode = statusCode;

            this.Headers.Add(HttpHeader.ContentLength, this.FileData.Length.ToString());
            this.Headers.Add(HttpHeader.ContentDisposition, "attachment");
        }

        public HttpStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; set; }

        public IHttpCookieCollection Cookies { get; set; }

        public byte[] FileData { get; }

        private void ValidateStatusCode(HttpStatusCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;

            if (299 > statusCodeNumber && statusCodeNumber < 400)
            {
                throw new InvalidResponseException("File response needs a status code above 300 and below 400.");
            }
        }
    }
}