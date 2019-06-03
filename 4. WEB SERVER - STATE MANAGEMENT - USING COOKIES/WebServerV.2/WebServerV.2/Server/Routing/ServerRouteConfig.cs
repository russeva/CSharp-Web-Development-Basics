namespace WebServerV._2.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Enums;
    using Routing.Contracts;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();

            }
            this.InitializeRoutingConfig(appRouteConfig);
        }
        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes => this.routes;



        private void InitializeRoutingConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var regRoutes in appRouteConfig.Routes)
            {
                var routeHandler = regRoutes.Value;
                var reqMethod = regRoutes.Key;

                foreach (var requestHandler in routeHandler)
                {
                    var route = requestHandler.Key;
                    var handler = requestHandler.Value;

                    List<string> parameters = new List<string>();

                    string parsedRegex = this.ParseRoute(route, parameters);
                    var routingCOntex = new RoutingContext(handler, parameters);
                    this.routes[reqMethod].Add(parsedRegex, routingCOntex);
                }
            }

           
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            StringBuilder result = new StringBuilder();
            result.Append("^");

            if (route == "/")
            {
                result.Append("/$");
                return result.ToString();
            }
            result.Append("/");

            var tokens = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            this.ParseTokens(parameters, tokens, result);
            return result.ToString();

            
        }

        private void ParseTokens(List<string> parameters, string[] tokens, StringBuilder result)
        {
            for (int i = 0; i < tokens.Length; i++)
            {

                string end = i == tokens.Length - 1 ? "$" : "/";

                var currentToken = tokens[i];
                if (!currentToken.StartsWith('{') && !currentToken.EndsWith('}'))
                {

                    result.Append($"{currentToken}{end}");
                    continue;
                }


                var parameterRegEx = new Regex("<\\w+>");
                var parameterMatch = parameterRegEx.Match(currentToken);

                if (!parameterMatch.Success)
                {

                    throw new InvalidOperationException($"Route parameter in {currentToken} is not valid");

                }

                var match = parameterMatch.Value;
                var parameter = match.Substring(1, match.Length - 2);


                parameters.Add(parameter);

                var currentTokenWithoutCurlyBrackets = currentToken.Substring(1, currentToken.Length - 2);

                result.Append($"{currentTokenWithoutCurlyBrackets}{end}");
            }
        }
    }
}
