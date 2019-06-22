using System;
using System.Collections.Generic;
using System.Net;namespace WebServerV._2.Application.Controllers
{
    using System.Text;
    using WebServerV._2.Application.Views;
    using WebServerV._2.Application.Views.Home;
    using WebServerV._2.Server.Enums;
    using WebServerV._2.Server.Http;
    using WebServerV._2.Server.Http.Contracts;
    using WebServerV._2.Server.Http.Response;

    class HomeController
    {
        /*  GET   /    */
        public IHttpResponse Index()
        {
            var response = new ViewResponse(HttpResponseStatusCode.Ok, new IndexView());
            response.CookieCollection.Add(new HttpCookie("lang","en"));

            return response;
        }

        /* GET   /testsession      */
        public IHttpResponse SessionTest(IHttpRequest request)
        {
            var session = request.Session;
            const string sessionDateKey = "saved_date"; 

            if(session.Get(sessionDateKey) == null)
            {
                session.Add(sessionDateKey, DateTime.UtcNow);
            }

            var response = new ViewResponse
                (HttpResponseStatusCode.Ok, 
                new SessionTestView(session.Get<DateTime>(sessionDateKey)));

            return response;
        }
    }
}
