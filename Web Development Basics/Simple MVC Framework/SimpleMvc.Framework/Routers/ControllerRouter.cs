namespace SimpleMvc.Framework.Routers
{
    using Attributes.Methods;
    using Contracts;
    using Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ControllerRouter : IHandleable
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            var getParams = request.UrlParameters;
            var postParams = request.FormData;
            var requestMethod = request.Method.ToString();

            var urlTokens = request
                .Path
                .Split(new char[] { '/', '?' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlTokens.Length < 2)
            {
                throw new InvalidOperationException("Invalid URL");
            }

            var controllerName =
                    char.ToUpper(urlTokens[0][0]) +
                    urlTokens[0].Substring(1) +
                    MvcContext.Get.ControllersSuffix;

            var actionName =
                    char.ToUpper(urlTokens[1][0]) +
                    urlTokens[1].Substring(1);

            var controller = this.GetController(controllerName);
            if (controller != null)
            {
                controller.Request = request;
                controller.InitializeController();
            }

            var method = this.GetMethod(controller, requestMethod, actionName);

            if (method == null)
            {
                return new NotFoundResponse();
            }

            IEnumerable<ParameterInfo> paramaters = method.GetParameters();

            object[] methodParams = this.AddParameters(paramaters, getParams, postParams);

            try
            {
                var response = this.GetResponse(method, controller, methodParams);
                return response;
            }
            catch (Exception ex)
            {
                return new InternalServerErrorResponse(ex);
            }
        }   

        private MethodInfo GetMethod(Controller controller, string requestMethod, string actionName)
        {
            MethodInfo method = null;
            foreach (MethodInfo methodInfo in this.GetSuitableMethods(controller, actionName))
            {
                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute);

                if (!attributes.Any() && requestMethod == "GET")
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods(Controller controller, string actionName)
        {
            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return
                controller
                .GetType()
                .GetMethods()
                .Where(m => m.Name.ToLower() == actionName.ToLower());
        }

        private Controller GetController(string controllerName)
        {
            var controllerFullQualifiedName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                controllerName);

            Type type = Type.GetType(controllerFullQualifiedName);

            if (type == null)
            {
                return null;
            }

            var controller = (Controller)Activator.CreateInstance(type);

            return controller;
        }

        private object[] AddParameters(
            IEnumerable<ParameterInfo> parameters,
            IDictionary<string, string> getParams,
            IDictionary<string ,string> postParams)
        {
            object[] methodParams = new object[parameters.Count()];

            var index = 0;

            foreach (ParameterInfo parameter in parameters)
            {
                if (parameter.ParameterType.IsPrimitive ||
                    parameter.ParameterType == typeof(string))
                {
                    methodParams[index] = this.ProcessPrimitiveParameter(parameter, getParams);
                    index++;
                }
                else
                {
                    methodParams[index] = this.ProcessComplexParameter(parameter, postParams);
                    index++;
                }
            }

            return methodParams;
        }

        private object ProcessPrimitiveParameter(
            ParameterInfo parameter, 
            IDictionary<string, string> getParams)
        {
            object value = getParams[parameter.Name];
            return Convert.ChangeType(value, parameter.ParameterType);
        }

        private object ProcessComplexParameter(
            ParameterInfo parameter,
            IDictionary<string, string> postParams)
        {
            var modelType = parameter.ParameterType;
            var model = Activator.CreateInstance(modelType);

            IEnumerable<PropertyInfo> properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var value = Convert.ChangeType(postParams[property.Name], property.PropertyType);

                property.SetValue(model, value);
            }

            return Convert.ChangeType(model, modelType);
        }

        private IHttpResponse GetResponse(
            MethodInfo method,
            Controller controller,
            object[] methodParams)
        {
            var actionResult = method.Invoke(controller, methodParams);

            IHttpResponse response = null;

            if (actionResult is IViewable)
            {
                var content = ((IViewable)actionResult).Invoke();

                response = new ContentResponse(HttpStatusCode.Ok, content);
            }
            else if (actionResult is IRedirectable)
            {
                var redirectUrl = ((IRedirectable)actionResult).Invoke();

                response = new RedirectResponse(redirectUrl);
            }

            return response;
        }
    }
}