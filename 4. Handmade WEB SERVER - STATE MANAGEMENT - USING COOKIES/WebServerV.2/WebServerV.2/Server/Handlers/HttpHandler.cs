namespace WebServerV._2.Server.Handlers
{
    
    using Handlers.Contracts;
    using Routing.Contracts;
    using System.Text.RegularExpressions;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Http.Contracts;
    using WebServerV._2.Server.Http.Response;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig routeConfig)
        {
            CoreValidator.ThrowIfNull(routeConfig, nameof(routeConfig));
            this.serverRouteConfig = routeConfig;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            var requestMethod = context.Request.Method;
            var requestPath = context.Request.Path;
            var registeredRoutes = this.serverRouteConfig.Routes[requestMethod];

            foreach (var registeredRoute in registeredRoutes)
            {
               
                var routePattern = registeredRoute.Key;
                var routingContext = registeredRoute.Value;
                
                Regex routeRegex = new Regex(routePattern);
                var match = routeRegex.Match(requestPath);

                if (!match.Success)
                {
                    continue;
                }

                
                var parameters = routingContext.Parameters;
                foreach (var parameter in parameters)
                {
                    
                    var parameterValue = match.Groups[parameter].Value;
                    context.Request.AddUrlParameter(parameter, parameterValue);
                }
                return routingContext.RequestHandler.Handle(context);
            }

            return new NotFoundResponse();
        }
    }
}
