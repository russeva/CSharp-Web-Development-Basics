namespace WebServerV._2.Server.Common
{
    using System;
    using Server.Http.Contracts;

    public class NotFoundView : IView
    {
        public string View()
        {
            return "<h1>404 This page does not exist ;( </h1>";
        }
    }
}
