namespace WebServerV._2.Server.Http.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Enums;
    using WebServerV._2.Server.Http.Contracts;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.HeaderCollection = new HttpHeaderCollection();
        }

        public HttpHeaderCollection HeaderCollection { get; }

        public HttpResponseStatusCode StatusCode { get; protected set; }

        private string statusCodeMessage { get => this.StatusCode.ToString(); }

        public override string ToString()
        {
            var response = new StringBuilder();
            var statusCodeNumber = this.StatusCode;

            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.statusCodeMessage}");
            response.AppendLine(this.HeaderCollection.ToString());
            response.AppendLine();

            return response.ToString();
        }
    }
}
