namespace WebServerV._2.Server.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Handlers.Contracts;
    using WebServerV._2.Server.Http;
    using WebServerV._2.Server.Http.Contracts;

    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            string sessionIdToSend = null;

            if (!httpContext.Request.CookieCollection.ContainsKey(SessionStore.SessionCookieKey))
            {
                var sessionId = Guid.NewGuid().ToString();
                httpContext.Request.Session = SessionStore.Get(sessionId);

                sessionIdToSend = sessionId;
            }

            IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);
            if(sessionIdToSend != null)
            {
                httpResponse
                   .HeaderCollection
                   .Add(HttpHeader.SetCookie,
                   $"{SessionStore.SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");
            }


            if (!httpResponse.HeaderCollection.ContainsKey(HttpHeader.ContentType))
            {
                httpResponse.HeaderCollection.Add(HttpHeader.ContentType, "text-plain");
            }

            foreach (var cookie in httpResponse.CookieCollection)
            {
                if (cookie.IsNew)
                {
                    httpResponse.HeaderCollection.Add(HttpHeader.SetCookie, cookie.ToString());
                }
            }

            return httpResponse;
        }
    }
}
