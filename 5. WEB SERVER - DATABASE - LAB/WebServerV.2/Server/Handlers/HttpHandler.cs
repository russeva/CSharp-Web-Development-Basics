namespace WebServerV._2.Server.Handlers
{
    
    using Handlers.Contracts;
    using Routing.Contracts;
    using System;
    using System.Text.RegularExpressions;

    using Server.Common;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;

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
            try
            {
                 var anonymousPaths = new[] { "/login", "/register"};

                var loginPath = "/login";

                var path = context.Request.Path != loginPath;

                if (!anonymousPaths.Contains(context.Request.Path) &&
                     (context.Request.Session == null || !context.Request.Session.Contains(SessionStore.CurrentUserKey)))
                {
                    return new RedirectResponse(anonymousPaths.First());
                }
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

                
            
            }
            catch(Exception ex)
            {
                new InternalServerErrorResposne(ex);
            }

            return new NotFoundResponse();
        }
    }
}
