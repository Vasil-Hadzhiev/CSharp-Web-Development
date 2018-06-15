namespace SimpleMvc.Framework.Routers
{
    using Attributes.Methods;
    using Controllers;
    using Interfaces;
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
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodPrams;

        public IHttpResponse Handle(IHttpRequest request)
        {
            this.getParams = new Dictionary<string, string>(request.UrlParameters);
            this.postParams = new Dictionary<string, string>(request.FormData);
            this.requestMethod = request.Method.ToString().ToUpper();

            this.PrepareControllerAndActionNames(request);

            MethodInfo method = this.GetMethod();

            if (method == null)
            {
                return new NotFoundResponse();
            }

            this.PrepareMethodParameters(method);

            return this.PrepareResponse();
        }   

        private void PrepareControllerAndActionNames(IHttpRequest request)
        {
            var urlTokens = request
                .Path
                .Split(new char[] { '/', '?' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlTokens.Length < 2)
            {
                throw new InvalidOperationException("Invalid URL");
            }

            this.controllerName = 
                char.ToUpper(urlTokens[0][0]) + 
                urlTokens[0].Substring(1) +
                MvcContext.Get.ControllersSuffix;

            this.actionName = 
                char.ToUpper(urlTokens[1][0]) + 
                urlTokens[1].Substring(1);
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;
            foreach (MethodInfo methodInfo in this.GetSuitableMethods())
            {
                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute);

                if (!attributes.Any() && this.requestMethod == "GET")
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetController();

            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return
                controller
                .GetType()
                .GetMethods()
                .Where(m => m.Name == this.actionName);
        }

        private Controller GetController()
        {
            var controllerFullQualifiedName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                this.controllerName);

            Type type = Type.GetType(controllerFullQualifiedName);

            if (type == null)
            {
                return null;
            }

            var controller = (Controller)Activator.CreateInstance(type);

            return controller;
        }

        private void PrepareMethodParameters(MethodInfo method)
        {
            IEnumerable<ParameterInfo> parameters = method.GetParameters();

            this.methodPrams = new object[parameters.Count()];

            var index = 0;
            foreach (ParameterInfo param in parameters)
            {
                if (param.ParameterType.IsPrimitive || 
                    param.ParameterType == typeof(string))
                {
                    object value = this.getParams[param.Name];
                    this.methodPrams[index] = Convert.ChangeType(
                        value,
                        param.ParameterType);

                    index++;
                }
                else
                {
                    Type bindingModelType = param.ParameterType;
                    object bindingModel = Activator.CreateInstance(bindingModelType);

                    IEnumerable<PropertyInfo> properties = bindingModelType.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        var postParamValue = this.postParams[property.Name];

                        var value = Convert.ChangeType(
                                postParamValue,
                                property.PropertyType);

                        property.SetValue(
                            bindingModel,
                            value);
                    }

                    this.methodPrams[index] = Convert.ChangeType(
                        bindingModel,
                        bindingModelType);

                    index++;
                }
            }
        }

        private IHttpResponse PrepareResponse()
        {
            IInvocable actionResult = (IInvocable)this.GetMethod()
                .Invoke(this.GetController(), this.methodPrams);

            var content = actionResult.Invoke();

            IHttpResponse response = new ContentResponse(HttpStatusCode.Ok, content);

            return response;
        }
    }
}