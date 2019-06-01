namespace WebServerV._2.Server.Http.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IHttpSession
    {
        string Id { get; }

        object Get(string key);

        T Get<T>(string key);

        void Add(string key, object value);

        void Clear();
    }
}
