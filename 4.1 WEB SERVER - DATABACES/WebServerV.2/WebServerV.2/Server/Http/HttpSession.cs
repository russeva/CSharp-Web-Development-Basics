namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Http.Contracts;

    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> values;
        
        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this.Id = id;
            this.values = new Dictionary<string, object>();
        }

        public string Id {get; private set;}

        public void Add(string key, object value)
        {
            CoreValidator.ThrowIfNull(value, nameof(value));
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            this.values[key] = value;
        }

        public void Clear()
        => this.values.Clear();

        public bool Contains(string key) => this.values.ContainsKey(key);
        

        public object Get(string key)
        {
           if(!this.values.ContainsKey(key))
            {
                return null;
            }

            return this.values[key];
        }

        public T Get<T>(string key)
        =>  (T)this.Get(key);
    }
}
