namespace WebServerV._2.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Handlers;
    using WebServerV._2.Server.Routing.Contracts;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler handler, IEnumerable<string> parameters)
        {
            CoreValidator.ThrowIfNull(handler, nameof(handler));
            CoreValidator.ThrowIfNull(parameters, nameof(parameters));

            this.RequestHandler = handler;
            this.Parameters = parameters;

        }

        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler RequestHandler { get; private set; }
    }
}
