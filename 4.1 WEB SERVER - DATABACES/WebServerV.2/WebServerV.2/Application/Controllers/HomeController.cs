
namespace WebServerV._2.Application.Controllers
{
    using System.Text;
    using Application.Views;
    using Application.Views.Home;
    using Server.Enums;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System;
    using System.Collections.Generic;
    using System.Net;

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
