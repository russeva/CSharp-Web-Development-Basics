namespace WebServerV._2.ByTheCakeApplication.Controllers
{
    using System;
    using ByTheCakeApplication.Infrastructure;
    using Server.Http.Contracts;
    using WebServerV._2.ByTheCakeApplication.Models;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home\index");
           
        }

        public IHttpResponse AboutUs()
        {
            return this.FileViewResponse(@"home\aboutus");
        }

    }
}
