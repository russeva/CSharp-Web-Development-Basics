namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Http.Contracts;

    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));
            this.cookies[cookie.Key] = cookie;
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));
            this.Add(new HttpCookie(key,value));
        }

        public bool ContainsKey(string cookie)
        {
            return this.cookies.ContainsKey(cookie);
        }

        public HttpCookie GetCookieHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.cookies.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the cookies collection");
            }
            return this.cookies[key];
            
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        => this.cookies.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        => this.cookies.Values.GetEnumerator();
    }
}
