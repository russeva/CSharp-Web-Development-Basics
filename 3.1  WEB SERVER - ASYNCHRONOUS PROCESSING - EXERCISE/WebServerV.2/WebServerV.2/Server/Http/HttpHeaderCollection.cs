﻿namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Http.Contracts;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        public readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        
        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header,nameof(header));

            headers[header.Key] = header;
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key,nameof(key));

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if(!this.headers.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} is not present in the headers collection");
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            return string.Join("\n",this.headers);
        }
    }
}
