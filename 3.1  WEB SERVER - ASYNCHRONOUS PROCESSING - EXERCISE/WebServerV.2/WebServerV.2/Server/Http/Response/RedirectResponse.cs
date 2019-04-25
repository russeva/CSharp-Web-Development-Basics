namespace WebServerV._2.Server.Http.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Common;
    using WebServerV._2.Server.Enums;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpResponseStatusCode.Found;
            this.HeaderCollection.Add(new HttpHeader("Location", redirectUrl));
        }
    }
}
