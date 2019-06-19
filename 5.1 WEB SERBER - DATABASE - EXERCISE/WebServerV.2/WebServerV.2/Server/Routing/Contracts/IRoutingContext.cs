using System.Collections;
using System.Collections.Generic;
using WebServerV._2.Server.Handlers;

namespace WebServerV._2.Server.Routing.Contracts
{
    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        RequestHandler RequestHandler { get; }
    }
}
