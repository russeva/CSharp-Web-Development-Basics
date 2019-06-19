namespace WebServerV._2.ByTheCakeApplication.Controllers
{
    using Server.Http.Contracts;
    using ByTheCakeApplication.Infrastructure;
    using Server.Http.Response;
    using System.Collections.Generic;
    using Server.Http;
    using ByTheCakeApplication.Models;
    using ByTheCakeApplication.Services;
    using System;
    using WebServerV._2.ByTheCakeApplication.ViewModels.Account;

    public class AccountController : Controller
    {
        private readonly IUserService users;
        public AccountController()
        {
            this.users = new UserService();
        }

        private void SetDefaultViewData() => this.ViewData["authDisplay"] = "none";

        internal IHttpResponse Register()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(@"Account/register");
        }

        public IHttpResponse Register(IHttpRequest req,RegisterUserViewModel model)
        {
            this.SetDefaultViewData();

            if (model.Username.Length < 3
                || model.Password.Length < 3
                || model.ConfirmPassword != model.Password)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid user details";

                return this.FileViewResponse(@"account\register");
            }

            var success = this.users.Create(model.Username, model.Password);

            if (success)
            {
                this.LoginUser(req, model.Username);

                return new RedirectResponse("/");
            }
            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "This username is taken.";

                return this.FileViewResponse(@"account\register");
            }
        }

        public IHttpResponse Profile(IHttpRequest request)
        {
            if(!request.Session.Contains(SessionStore.CurrentUserKey))
            {
                throw new InvalidOperationException("There is no logged in user.");
            }

            var username = request.Session.Get<string>(SessionStore.CurrentUserKey);
            var profile = this.users.Profile(username);
            
            if(profile == null)
            {
                throw new InvalidOperationException($"The given user {username} could not be found in the database.");
            }
            else
            {
                this.ViewData["username"] = profile.Username;
                this.ViewData["registeredOn"] = profile.RegistrationDate.ToShortDateString();
                this.ViewData["ordersCount"] = profile.TotalOrders.ToString();
                return this.FileViewResponse(@"account\profile");
            }
            
        }

        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"Account/login");

        }

        public IHttpResponse Login(IHttpRequest request, LoginViewModel model)
        {
            
            if (string.IsNullOrWhiteSpace(model.Username)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                this.ViewData["error"] = "You have empty fields.";
                this.ViewData["showError"] = "block";

                return this.FileViewResponse(@"Account\login");
                
            }

            var success = this.users.FindUser(model.Username, model.Password);
            if(success)
            {
                this.LoginUser(request, model.Username);
                return new RedirectResponse("/");
            }
            else
            {
                this.ViewData["error"] = "Invalid user details";
                this.ViewData["showError"] = "block";

                return this.FileViewResponse(@"Account\login");
            }
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.Clear();

            return new RedirectResponse("/login");
        }

        private void LoginUser(IHttpRequest request, string username)
        {
            request.Session.Add(SessionStore.CurrentUserKey, username);
            request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }

    }
}
