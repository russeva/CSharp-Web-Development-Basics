namespace WebServerV._2.ByTheCakeApplication.Infrastructure
{
    using System.IO;

    using Server.Http.Contracts;
    using Server.Http.Response;
    using Server.Enums;
    using ByTheCakeApplication.Views.Home;
    using System.Collections.Generic;
    using System.Linq;

    public  abstract class Controller
    {
       
        public const string defaultPath = @"..\..\..\ByTheCakeApplication\Resources\{0}.html";
        protected IDictionary<string, string> ViewData { get; private set; }

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>()
            {
                ["authDisplay"] = "block",
                ["showError"] = "none",
                
            };
        }


        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProcessFileHtml(fileName);

            if(this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpResponseStatusCode.Ok, new FileView(result));
        }

        private string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(defaultPath, "layout"));

            var fileHtml = File.ReadAllText(string.Format(defaultPath, fileName));

            var result = layoutHtml.Replace("{{{content}}}", fileHtml);

            return result;
        }
    }
}
