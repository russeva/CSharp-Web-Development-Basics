using System;
using System.Collections.Generic;
using System.Text;
using WebServerV._2.Server.Http.Contracts;

namespace WebServerV._2.Application.Views.Home
{
      public class IndexView : IView
        {
            public string View() => "<h1>Welcome<h1>";

        }
    
}
