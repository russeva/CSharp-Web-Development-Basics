namespace WebServerV._2.ByTheCakeApplication.Controllers
{
    using Server.Http.Contracts;
    using ByTheCakeApplication.Infrastructure;
    using Server.Http.Response;
    using System.Collections.Generic;
    using Server.Http;
    using ByTheCakeApplication.Models;

    public class AccountController : Controller
    {

        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"Account/login");

        }
        

        public IHttpResponse Login(IHttpRequest request)
        {

            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if(!request.FormData.ContainsKey(formNameKey)
                || !request.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var name = request.FormData[formNameKey];
            var password = request.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = "You have empty fields.";
                this.ViewData["showError"] = "block";

                return this.FileViewResponse(@"Account/login");
                
            }

            request.Session.Add(SessionStore.CurrentUserKey, name);
            request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
            
            return new RedirectResponse("/");
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.Clear();

            return new RedirectResponse("/login");
        }
    }
}
