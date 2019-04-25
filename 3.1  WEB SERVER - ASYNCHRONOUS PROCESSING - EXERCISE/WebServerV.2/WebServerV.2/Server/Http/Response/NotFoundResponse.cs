namespace WebServerV._2.Server.Http.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Enums;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
        {
            this.StatusCode = HttpResponseStatusCode.NotFound;
        }
    }
}
