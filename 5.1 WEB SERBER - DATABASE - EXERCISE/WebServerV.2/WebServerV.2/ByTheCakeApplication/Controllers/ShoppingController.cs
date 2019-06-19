namespace WebServerV._2.ByTheCakeApplication.Controllers
{
    using System;
    using ByTheCakeApplication.Infrastructure;
    using Server.Http.Contracts;
    using ByTheCakeApplication.Data;
    using Server.Http.Response;
    using ByTheCakeApplication.Models;
    using System.Linq;
    using ByTheCakeApplication.Services;
    using WebServerV._2.Server.Http;

    public class ShoppingController : Controller
    {
        private readonly ICakeService cakes;
        private readonly IUserService users;
        private readonly IShoppingService shopping;

        public ShoppingController()
        {
            this.cakes = new CakeService();
            this.users = new UserService();
            this.shopping = new ShoppingService();
        }

        public IHttpResponse AddToCart(IHttpRequest request)
        {
            var id = int.Parse(request.UrlParameters["id"]);

            var cakeExist = this.cakes.Exists(id);
            
            if(!cakeExist)
            {
                return new NotFoundResponse();
            }
            else
            {
                var cakeView = this.cakes.Find(id);
                var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
                shoppingCart.OrderIds.Add(id);
            }

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

            if (!shoppingCart.OrderIds.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var cakesIncart = this.cakes.FindProductsInCart(shoppingCart.OrderIds);

                var items = cakesIncart
                    .Select(pr => $"<div>{pr.Name} - ${pr.Price:f2}</div><br />");

                this.ViewData["cartItems"] = string.Join(string.Empty, items);

                var totalPrice = cakesIncart
                    .Sum(pr => pr.Price);

                this.ViewData["totalCost"] = $"{totalPrice:f2}";
            }


            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest request)
        {
            var username = request.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppinCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            var userId = this.users.GetUserId(username);
            var productIds = shoppinCart.OrderIds;
            if(userId == null)
            {
                throw new InvalidOperationException($"User {username} does not exist");
            }
            if(!productIds.Any())
            {
                return new RedirectResponse("/");
            }

            this.shopping.CreateOrder(userId.Value, productIds);
            
            shoppinCart.OrderIds.Clear();
            
            return this.FileViewResponse(@"shopping\finish-order");
        }

        public IHttpResponse OrderDetails(IHttpRequest request)
        {
            var username = request.Session.Get<string>(SessionStore.CurrentUserKey);
            var order = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            
            //to be implemented
            
            this.ViewData["authDisplay"] = "none";
            return this.FileViewResponse(@"shopping\order-details");
        }
    }
}
