namespace WebServerV._2.ByTheCakeApplication.Controllers
{
    using System;
    using ByTheCakeApplication.Infrastructure;
    using Server.Http.Contracts;
    using ByTheCakeApplication.Data;
    using Server.Http.Response;
    using WebServerV._2.ByTheCakeApplication.Models;
    using System.Linq;

    public class ShoppingController : Controller
    {
        private readonly CakesData CakeData;

        public ShoppingController()
        {
            this.CakeData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest request)
        {
            var id = int.Parse(request.UrlParameters["id"]);

            var cake = CakeData.Find(id);
            
            if(cake == null)
            {
                return new NotFoundResponse();
            }

           var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
           shoppingCart.Orders.Add(cake);

            var redirectUrl = "/search";

            const string searchTerm = "searchTerm";
            if(request.QueryParameters.ContainsKey(searchTerm))
            {
                redirectUrl = $"{redirectUrl}?{searchTerm}={request.QueryParameters[searchTerm]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest request)
        {
            var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.Orders.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var items = shoppingCart
                    .Orders
                    .Select(i => $"<div>{i.Name.Replace('+',' ')} - ${i.Price:f2}</div><br />");

                this.ViewData["cartItems"] = string.Join(string.Empty, items);

                var totalPrice = shoppingCart
                    .Orders
                    .Sum(i => i.Price);

                this.ViewData["totalCost"] = $"{totalPrice:f2}";
            }


            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder()
        {
            return this.FileViewResponse(@"shopping\finish-order");
        }

    }
}
