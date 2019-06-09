namespace WebServerV._2.ByTheCakeApplication.Controllers
{
    using ByTheCakeApplication.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ByTheCakeApplication.Data;
    using ByTheCakeApplication.Models;
    using Server.Http.Contracts;

    public class CakesController : Controller
    {
        private readonly CakesData cakesData;

        public CakesController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            this.ViewData["display"] = "none";

            return this.FileViewResponse(@"Cakes\addcake");
        }

        public IHttpResponse Add(string cake, string price)
        {
            var newCake = new Cake()
            { 
                Name = cake.Replace('+',' '),
                Price = decimal.Parse(price)
            };
            
            this.cakesData.Add(cake, price);

            this.ViewData["name"] = cake.Replace('+',' ');
            this.ViewData["price"] = price;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(@"Cakes\addcake");
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            const string searchTermKey = "searchTerm";
            var urlParams = request.UrlParameters;

            var results = string.Empty;
            this.ViewData["results"] = results;
            this.ViewData["searchTerm"] = string.Empty;

            if (request.QueryParameters.ContainsKey("searchTerm"))
            {
                var searchTerm = request.QueryParameters[searchTermKey];
                this.ViewData["searchTerm"] = searchTerm;
                
                var savedCakesDivs = this.cakesData
                    .All()
                    .Where(c => c.Name.ToLower()
                    .Contains(searchTerm.ToLower()))
                    .Select(c => $@"<div>{c.Name.Replace('+',' ')} - ${c.Price:f2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>");

                results = string.Join(Environment.NewLine, savedCakesDivs);
                this.ViewData["results"] = results;
            }
            else
            {
                this.ViewData["results"] = "No cakes found.";
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"Cakes\search");
        }
    }

    
}
