namespace WebServerV._2.Server.Http.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IHttpContext
    {
        IHttpRequest Request { get; }
    }
}
