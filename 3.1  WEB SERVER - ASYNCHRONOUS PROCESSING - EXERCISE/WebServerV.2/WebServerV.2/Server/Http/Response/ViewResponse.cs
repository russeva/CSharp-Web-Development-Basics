namespace WebServerV._2.Server.Http.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServerV._2.Server.Enums;
    using WebServerV._2.Server.Http.Contracts;

    public class ViewResponse : HttpResponse
    {

        private readonly IView view;

        private readonly HttpResponseStatusCode statusCode;

        public ViewResponse(HttpResponseStatusCode statusCode, IView view)
        {
            this.ValidateStatusCode(statusCode);
            this.view = view;
            this.statusCode = statusCode;
        }

        private void ValidateStatusCode(HttpResponseStatusCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;
            if(statusCodeNumber > 300 && statusCodeNumber < 399)
            {
                throw new InvalidOperationException("View response need status code below 300 ");
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} {this.view.View()}";
        }

    }
}
