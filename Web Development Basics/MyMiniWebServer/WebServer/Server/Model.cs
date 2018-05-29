namespace WebServer.Server
{
    using System.Collections.Generic;

    public class Model
    {
        private readonly Dictionary<string, object> models;

        public object this[string key]
        {
            get => this.models[key];
            set { this.models[key] = value; }
        }

        public Model()
        {
            this.models = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            this.models[key] = value;
        }
    }
}