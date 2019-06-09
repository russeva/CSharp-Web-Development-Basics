using System;
using WebServerV._2.Application.Controllers;
using WebServerV._2.Server.Contracts;
using WebServerV._2.Server.Enums;
using WebServerV._2.Server.Handlers;
using WebServerV._2.Server.Routing.Contracts;

namespace WebServerV._2
{
    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
			.Get("/",req => new HomeController().Index());

           appRouteConfig
			.Get("/testsession", req => new HomeController().SessionTest(req)); 
			

        }
    }
}