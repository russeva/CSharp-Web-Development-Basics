namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Common;

    public class HttpHeader
    {
        public HttpHeader(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public string Key { get;  private set; }

        public string Value { get; private set; }

        public override string ToString()
        {
            return this.Key + ": " + this.Value;
        }
    }
}
