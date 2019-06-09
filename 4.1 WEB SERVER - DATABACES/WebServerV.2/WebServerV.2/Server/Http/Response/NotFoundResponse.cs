namespace WebServerV._2.Server.Http.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Server.Enums;
    using Server.Common;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse()
            :base(HttpResponseStatusCode.NotFound,new NotFoundView())
        {
        }
    }
}
