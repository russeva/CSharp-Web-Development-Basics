namespace WebServerV._2.Server.Handlers
{
    using System;
    using WebServerV._2.Server.Http.Contracts;

    public class GetHandler : RequestHandler
    {
        public GetHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) 
            : base(handlingFunc)
        {
        }
    }
}
