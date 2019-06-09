namespace WebServerV._2.Server.Http.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Enums;
    using WebServerV._2.Server.Http.Contracts;

    public class HttpResponse : IHttpResponse
    {
        private string statusCodeMessage { get => this.StatusCode.ToString(); }

        public HttpResponse()
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.CookieCollection = new HttpCookieCollection();
        }

        public IHttpHeaderCollection HeaderCollection { get; }

        public IHttpCookieCollection CookieCollection { get; }

        public HttpResponseStatusCode StatusCode { get; protected set; }

        public override string ToString()
        {
            var response = new StringBuilder();
            var statusCodeNumber = (int)this.StatusCode;

            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.statusCodeMessage}");
            response.AppendLine(this.HeaderCollection.ToString());
            

            return response.ToString();
        }
    }
}
