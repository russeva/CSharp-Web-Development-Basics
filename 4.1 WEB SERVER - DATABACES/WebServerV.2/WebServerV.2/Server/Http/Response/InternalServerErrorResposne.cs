namespace WebServerV._2.Server.Http.Response
{
    using System;
    using Server.Http.Contracts;

    public class InternalServerErrorResposne : HttpResponse
    {
        public InternalServerErrorResposne(Exception ex)
        {
            this.StatusCode = Enums.HttpResponseStatusCode.InternalServerError;
        }
    }
}
