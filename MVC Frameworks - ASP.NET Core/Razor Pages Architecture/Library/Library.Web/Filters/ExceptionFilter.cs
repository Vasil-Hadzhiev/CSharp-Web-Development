namespace Library.Web.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using System;

    public class ExceptionFilter : IExceptionFilter
    {
        private ILogger logger;

        public ExceptionFilter(ILogger logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            context.ExceptionHandled = true;
            this.logger.LogInformation($"Exception {ex}");
        }
    }
}