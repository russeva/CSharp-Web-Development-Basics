using System;
using System.Collections.Generic;
using System.Text;
using WebServerV._2.Server.Routing.Contracts;

namespace WebServerV._2.Server.Contracts
{
    public interface IApplication
    {
        void Start(IAppRouteConfig appRouteConfig);

    }
}
