namespace WebServerV._2.Server.Routing.Contracts
{
    using System;
    using System.Collections.Generic;

    using WebServerV._2.Server.Enums;
    using WebServerV._2.Server.Handlers;
    using WebServerV._2.Server.Http.Contracts;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes { get; }
        

        void Get(string route, Func<IHttpRequest, IHttpResponse> handler);

        void Post(string route, Func<IHttpRequest, IHttpResponse> handler);

        void AddRoute(string route, HttpRequestMethod requestMethod, RequestHandler handler);
    }
}
