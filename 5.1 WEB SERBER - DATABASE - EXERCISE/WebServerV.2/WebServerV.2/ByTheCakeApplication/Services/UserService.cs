namespace WebServerV._2.ByTheCakeApplication.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebServerV._2.ByTheCakeApplication.Data;
    using WebServerV._2.ByTheCakeApplication.Data.Models;
    using WebServerV._2.ByTheCakeApplication.ViewModels.Account;

    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {

            using (var db = new ByTheCakeDbContext())
            {
                if(db.Users.Any(u => u.Username == username))
                {
                    return false;
                }
                User newUser = new User()
                {
                    Username = username,
                    Password = password,
                    DateOfRegistration = DateTime.Now
                };

                db.Add(newUser);
                db.SaveChanges();
            }
            return true;
        }

        public bool FindUser(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        public int? GetUserId(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var id = db.Users
                    .Where(u => u.Username == username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                return id != 0 ? (int?)id : null; 
            }
           
        }

        public ProfileViewModel Profile(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Users
                    .Where(u => u.Username == username)
                    .Select(u => new ProfileViewModel()
                    {
                        Username = u.Username,
                        RegistrationDate = u.DateOfRegistration,
                        TotalOrders = u.Orders.Count()
                    })
                    .FirstOrDefault();
            }
        }
    }
}
