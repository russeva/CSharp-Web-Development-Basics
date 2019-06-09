namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Http.Contracts;

    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(IHttpRequest requestStr)
        {
            this.request = requestStr;
        }

        public IHttpRequest Request  => this.request; 
    }
}
