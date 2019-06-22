namespace WebServerV._2.Server.Http.Contracts
{
    using System.Collections.Generic;

    public interface IHttpCookieCollection : IEnumerable<HttpCookie>
    {
        void Add(HttpCookie header);

        void Add(string key, string value);

        bool ContainsKey(string key);

        HttpCookie GetCookieHeader(string key);
        
    }
}
