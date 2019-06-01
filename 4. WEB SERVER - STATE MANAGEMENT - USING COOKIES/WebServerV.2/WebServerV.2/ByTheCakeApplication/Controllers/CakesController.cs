namespace WebServerV._2.ByTheCakeApplication.Controllers
{
    using ByTheCakeApplication.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using WebServerV._2.ByTheCakeApplication.Models;
    using WebServerV._2.Server.Http.Contracts;

    public class CakesController : Controller
    {
        private static readonly List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
        {
            return this.FileViewResponse(@"Cakes\addcake", new Dictionary<string, string>
            {
                ["display"] = "none"
            });
        }

        public IHttpResponse Add(string cake, string price)
        {
            var newCake = new Cake()
            { 
                Name = cake,
                Price = decimal.Parse(price)
            };

            cakes.Add(newCake);

            //using (var streamWriter = new StreamWriter(@"ByTheCakeApplication\Data\database.csv",true))
            //{
            //    streamWriter.WriteLine($"{{{cake}}},{{{price}}}");
            //}

            return this.FileViewResponse(@"Cakes\addcake", new Dictionary<string, string>
            {
                ["name"] = cake,
                ["price"] = price,
                ["display"] = "block"
            });
        }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            const string searchTermKey = "searchTerm";

            var results = string.Empty;

            if (urlParameters.ContainsKey("searchTerm"))
            {
                var searchTerm = urlParameters[searchTermKey];

                var savedCakesDivs = File.ReadAllLines(@"ByTheCakeApplication\Data\database.csv")
                    .Where(line => line.Contains(','))
                    .Select(line => line.Split(','))
                    .Select(line => new Cake()
                    {
                        Name = line[0],
                        Price = decimal.Parse(line[1])
                    })
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $"<div>{c.Name} - ${c.Price}</div>");

                results = string.Join(Environment.NewLine, savedCakesDivs);
                
            }

            return this.FileViewResponse(@"Cakes\search", new Dictionary<string, string>()
            {
                ["results"] = results
            });
              
        }
    }

    
}
