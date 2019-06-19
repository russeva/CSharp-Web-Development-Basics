namespace WebServerV._2.Server.Http.Response
{
    using System;
    using Server.Enums;

    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
        {
            this.StatusCode = HttpResponseStatusCode.BadRequest;
        }
    }
}
