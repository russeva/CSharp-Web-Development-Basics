namespace WebServerV._2.ByTheCakeApplication
{
    using System;
    using System.Collections.Generic;
    using WebServerV._2.ByTheCakeApplication.Controllers;
    using WebServerV._2.Server.Contracts;
    using WebServerV._2.Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/aboutus", req => new HomeController().AboutUs());

            appRouteConfig
                .Get("/addcake", req => new CakesController().Add());

            appRouteConfig
                .Post("/addcake", req => new CakesController().Add(req.FormData["name"],req.FormData["price"]));

            appRouteConfig
                .Get("/search", req => new CakesController().Search(req));
        }
    }
}
