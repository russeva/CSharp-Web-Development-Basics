namespace WebServerV._2.Server.Routing.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Enums;

    public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }
        
    }
}
