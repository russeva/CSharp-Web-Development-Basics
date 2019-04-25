namespace WebServerV._2
{
    using System;
    using WebServerV._2.Server;
    using WebServerV._2.Server.Contracts;
    using WebServerV._2.Server.Routing;
    using WebServerV._2.Server.Routing.Contracts;

    public class Launcher : IRunnable
    {
        private WebServer webserver;
       

        static void Main(string[] args)
        {
            new Launcher().Run();
        }
        public void Run()
        {
           
            var mainApplication = new MainApplication();
            IAppRouteConfig routeConfig = new AppRouteConfig();
            mainApplication.Start(routeConfig);

            this.webserver = new WebServer(8230, routeConfig);
            this.webserver.Run();
        }
    }

}
