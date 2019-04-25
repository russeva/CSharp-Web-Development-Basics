namespace WebServerV._2.Server.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Handlers.Contracts;
    using WebServerV._2.Server.Http;
    using WebServerV._2.Server.Http.Contracts;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);
            httpResponse.HeaderCollection.Add(new HttpHeader("Content-Type","text-plain"));

            return httpResponse;
        }
    }
}
