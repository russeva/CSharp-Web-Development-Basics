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
    using ByTheCakeApplication.ViewModels.Cake;
    using WebServerV._2.ByTheCakeApplication.Services;
    using WebServerV._2.Server.Http.Response;

    public class CakesController : Controller
    {
        private readonly ICakeService cakes;

        public CakesController()
        {
            this.cakes = new CakeService();
        }

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";
            
            return this.FileViewResponse(@"Cakes\addcake");
        }

        public IHttpResponse Add(AddCakeViewModel model)
        {
            if(model.Name.Length < 3 
                || model.Name.Length > 30
                || model.ImageUrl.Length < 3
                || model.ImageUrl.Length > 2000)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Product information is not valid.";

                return this.FileViewResponse(@"cakes\addcake");
            }

            this.cakes.Create(model.Name,model.Price,model.ImageUrl);

            this.ViewData["name"] = model.Name;
            this.ViewData["price"] = model.Price.ToString();
            this.ViewData["imageUrl"] = model.ImageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"Cakes\addcake");
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            const string searchTermKey = "searchTerm";
            var urlParams = request.UrlParameters;

            var results = string.Empty;

            this.ViewData["results"] = results;
            
            var searchTerm = urlParams.ContainsKey(searchTermKey)
                ? urlParams[searchTermKey]
                : null;

            this.ViewData["searchTerm"] = searchTerm;

            var result = this.cakes.All(searchTerm);

            if (!result.Any())
            {
                this.ViewData["results"] = "No cakes found.";
            }
            else
            {
                var allProducts = result
                    .Select(c => $@"<div><a href=""/cakeDetails/{c.Id}"">{c.Name}</a> - ${c.Price:f2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>");

                var allProductsAsString = string.Join(Environment.NewLine, allProducts);

                this.ViewData["results"] = allProductsAsString;
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart != null)
            {
                var totalProducts = shoppingCart.OrderIds.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"Cakes\search");
        }

        public IHttpResponse Details(int id)
        {
            var cake = this.cakes.Find(id);

            if(cake == null)
            {
                return new NotFoundResponse();
            }
            this.ViewData["name"] = cake.Name;
            this.ViewData["price"] =  cake.Price.ToString("F2");
            this.ViewData["imageUrl"] = cake.ImageUrl;
           
            return this.FileViewResponse(@"cakes\details");
        }
    }

    

    
}
