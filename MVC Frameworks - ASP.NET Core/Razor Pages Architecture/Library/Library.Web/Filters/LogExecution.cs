namespace Library.Web.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;

    public class LogExecution : IPageFilter, IActionFilter
    {
        private ILogger<LogExecution> logger;
        private Stopwatch stopwatch;

        public LogExecution(ILogger<LogExecution> logger, Stopwatch stopwatch)
        {
            this.logger = logger;
            this.stopwatch = stopwatch;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.LogLeavingMessage();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.LogEnteringMessage(
                context.HttpContext.Request.Method, 
                context.ActionDescriptor.DisplayName, 
                context.ModelState.IsValid);
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            this.LogLeavingMessage();
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            this.LogEnteringMessage(
                context.HttpContext.Request.Method,
                context.ActionDescriptor.DisplayName,
                context.ModelState.IsValid);
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }

        private void LogEnteringMessage(string httpMethod, string actionName, bool isModelStateValid)
        {
            this.stopwatch = new Stopwatch();
            this.logger.LogInformation($"Executing {httpMethod} {actionName}");
            this.logger.LogInformation($"Model state: {(isModelStateValid ? "valid" : "invalid")}");

            this.stopwatch.Start();
        }

        private void LogLeavingMessage()
        {
            this.stopwatch.Stop();
            var time = this.stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine(time);
        }
    }
}