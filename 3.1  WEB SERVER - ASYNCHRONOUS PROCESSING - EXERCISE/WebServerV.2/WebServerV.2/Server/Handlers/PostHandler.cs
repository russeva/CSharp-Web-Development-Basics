namespace WebServerV._2.Server.Handlers
{
    using System;
    using WebServerV._2.Server.Http.Contracts;

    public class PostHandler : RequestHandler
    {
        public PostHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) : base(handlingFunc)
        {
        }
    }
}
