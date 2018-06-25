namespace SimpleMvc.Framework.Controllers
{
    using ActionResults;
    using Attributes.Property;
    using Contracts;
    using Models;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Views;
    using WebServer.Http;
    using WebServer.Http.Contracts;

    public abstract class Controller
    {
        protected Controller()
        {
            this.Model = new ViewModel();
            this.User = new Authentication();
        }

        protected ViewModel Model { get; }

        protected internal IHttpRequest Request { get; internal set; }

        protected internal Authentication User { get; private set; }

        private void IntializeViewModelData()
        {
            this.Model["displayType"] = this.User.IsAuthenticated ? "block" : "none";
        }

        protected IViewable View([CallerMemberName] string caller = "")
        {
            this.IntializeViewModelData();

            var controllerName = this.GetType()
                    .Name
                    .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            var fullQualifiedName = string.Format("{0}\\{1}\\{2}",
                    MvcContext.Get.ViewsFolder,
                    controllerName,
                    caller);

            IRenderable view = new View(fullQualifiedName, this.Model.Data);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
        {
            return new RedirectResult(redirectUrl);
        }

        protected bool IsValid(object model)
        {
            var modelProperties = model.GetType().GetProperties();

            foreach (PropertyInfo property in modelProperties)
            {
                IEnumerable<Attribute> attributes = property
                    .GetCustomAttributes()
                    .Where(a => a is PropertyAttribute);

                if (!attributes.Any())
                {
                    continue;
                }

                foreach (PropertyAttribute attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(model)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected internal void InitializeController()
        {
            var user = this.Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            if (user != null)
            {
                this.User = new Authentication(user);
            }
        }

        protected void SignIn(string name)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, name);
        }

        protected void SignOut()
        {
            this.Request.Session.Clear();
        }
    }
}