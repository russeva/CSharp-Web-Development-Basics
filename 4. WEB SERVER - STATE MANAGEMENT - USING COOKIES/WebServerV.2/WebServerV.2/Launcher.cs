namespace WebServerV._2
{
    using System;
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using Server.Routing.Contracts;
    using ByTheCakeApplication;

    public class Launcher : IRunnable
    {
        private WebServer webserver;
       

        static void Main(string[] args)
        {
            new Launcher().Run();
        }
        public void Run()
        {

            // var mainApplication = new MainApplication();
            var mainApplication = new ByTheCakeApp();
            IAppRouteConfig routeConfig = new AppRouteConfig();
            mainApplication.Start(routeConfig);

            this.webserver = new WebServer(8230, routeConfig);
            this.webserver.Run();
        }
    }

}
