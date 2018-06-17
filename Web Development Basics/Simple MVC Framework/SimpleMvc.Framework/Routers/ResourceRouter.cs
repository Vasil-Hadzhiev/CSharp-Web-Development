namespace SimpleMvc.Framework.Routers
{
    using System;
    using System.IO;
    using System.Linq;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ResourceRouter : IHandleable
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            var fileFullName = request.Path.Split("/").Last();
            var fileExtension = request.Path.Split(".").Last();

            IHttpResponse fileResponse = null;

            try
            {
                byte[] fileContent = this.ReadFileContent(fileFullName, fileExtension);

                fileResponse = new FileResponse(HttpStatusCode.Ok, fileContent);
            }
            catch (Exception)
            {
                return new NotFoundResponse();
            }

            return fileResponse;
        }

        private byte[] ReadFileContent(string fileFullName, string fileExtension)
        {
            byte[] content = File.ReadAllBytes(string.Format(
                "{0}\\{1}\\{2}",
                MvcContext.Get.ResourcesFolder,
                fileExtension,
                fileFullName));

            return content;
        }
    }
}