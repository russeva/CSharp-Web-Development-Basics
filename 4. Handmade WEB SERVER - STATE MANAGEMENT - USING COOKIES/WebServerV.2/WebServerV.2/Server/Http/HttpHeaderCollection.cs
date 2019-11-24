﻿namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Http.Contracts;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Add(new HttpHeader(key,value));
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header,nameof(header));

            var headerKey = header.Key;
           // var headerValue = header.Value;

            if(!this.headers.ContainsKey(headerKey))
            {
                this.headers[headerKey] = new List<HttpHeader>(); 
            }

            this.headers[headerKey].Add(header);
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key,nameof(key));

            return this.headers.ContainsKey(key);
        }

        public ICollection<HttpHeader> GetHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if(!this.headers.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the headers collection");
            }

            return this.headers[key];
        }

        public IEnumerator<ICollection<HttpHeader>> GetEnumerator()
        => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        => this.headers.Values.GetEnumerator();

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var header in this.headers)
            {
                var headerKey = header.Key;
                foreach (var headerValue in header.Value)
                {
                    result.AppendLine($"{headerKey}: {headerValue.Value}");
                }
            }

            return result.ToString();
        }

        
    }
}