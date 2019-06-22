namespace WebServerV._2.ByTheCakeApplication
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using ByTheCakeApplication.Controllers;
    using ByTheCakeApplication.Data;
    using ByTheCakeApplication.ViewModels.Account;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using WebServerV._2.ByTheCakeApplication.ViewModels.Cake;

    public class ByTheCakeApp : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new ByTheCakeDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/aboutus", req => new HomeController().AboutUs());

            appRouteConfig
                .Get("/addcake", req => new CakesController().Add());

            appRouteConfig
                .Post("/addcake", req => new CakesController()
                .Add(new AddCakeViewModel()
                {
                    Name = req.FormData["name"],
                    Price = decimal.Parse(req.FormData["price"]),
                    ImageUrl = req.FormData["imageUrl"]
                }));

            appRouteConfig
                .Get("/search", req => new CakesController().Search(req));

            appRouteConfig
                 .Get("cakeDetails/{(?<id>[0-9]+)}", req => new CakesController().Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get("/register", req => new AccountController().Register());

            appRouteConfig
                .Post(
                    "/register",
                    req => new AccountController().Register(
                        req,
                        new RegisterUserViewModel
                        {
                            Username = req.FormData["username"],
                            Password = req.FormData["password"],
                            ConfirmPassword = req.FormData["confirm-password"]
                        }));

            appRouteConfig
                .Get("/login", req => new AccountController().Login());

            appRouteConfig
                .Post("/login", req => new AccountController().Login(
                    req,
                    new LoginViewModel()
                    {
                        Username = req.FormData["username"],
                        Password = req.FormData["password"]
                    }));
            appRouteConfig
                .Get("/profile", req => new AccountController().Profile(req));
            appRouteConfig
                .Post("/logout", req => new AccountController().Logout(req));

            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", req => new ShoppingController().AddToCart(req));

            appRouteConfig
                .Get("/cart", req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Get("/orderDetails/{(?<id>[0-9]+)}", req => new ShoppingController().OrderDetails(req));

            appRouteConfig
                .Post("/shopping/finish-order",req => new ShoppingController().FinishOrder(req));

            
        }           
    }
}
