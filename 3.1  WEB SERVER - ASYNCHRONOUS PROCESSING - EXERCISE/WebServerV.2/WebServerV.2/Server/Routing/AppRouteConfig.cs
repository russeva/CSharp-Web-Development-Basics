namespace WebServerV._2.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Enums;
    using Handlers;
    using Http.Contracts;
    using Routing.Contracts;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, RequestHandler>();
            }

        }
        public IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes => this.routes;

        public void AddRoute(string route, HttpRequestMethod requestMethod, RequestHandler handler)
        {
            this.routes[requestMethod].Add(route, handler);
        }

        public void Get(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, HttpRequestMethod.Get, new GetHandler(handler));
        }
        
        
        public void Post(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, HttpRequestMethod.Post, new PostHandler(handler));
        }
    }
}
