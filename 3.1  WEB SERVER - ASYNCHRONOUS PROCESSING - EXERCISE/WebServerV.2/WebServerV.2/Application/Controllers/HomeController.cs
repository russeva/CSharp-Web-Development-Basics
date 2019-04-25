using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebServerV._2.Application.Views.Home;
using WebServerV._2.Server.Http.Contracts;
using WebServerV._2.Server.Http.Response;

namespace WebServerV._2.Application.Controllers
{
    class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(Server.Enums.HttpResponseStatusCode.Ok, new IndexView());
        }
    }
}
