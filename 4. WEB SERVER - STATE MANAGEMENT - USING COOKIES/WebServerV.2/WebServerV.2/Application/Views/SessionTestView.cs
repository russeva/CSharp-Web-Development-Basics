namespace WebServerV._2.Application.Views
{
    using System;
    using System.Collections.Generic;
    using WebServerV._2.Server.Http.Contracts;

    public class SessionTestView : IView
    {
        private readonly DateTime datetime;
        
        public SessionTestView(DateTime datetime)
        {
            this.datetime = datetime;
        }

        public string View()
        {
           return $"<h1>Saved date {datetime}</h1>";
        }
    }
}
