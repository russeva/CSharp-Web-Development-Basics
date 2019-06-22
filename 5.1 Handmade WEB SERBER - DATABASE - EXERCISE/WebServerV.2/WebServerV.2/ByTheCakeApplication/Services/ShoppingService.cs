namespace WebServerV._2.ByTheCakeApplication.Services
{
    using ByTheCakeApplication.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebServerV._2.ByTheCakeApplication.Data.Models;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int> productIds)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = new Order()
                {
                    UserId = userId,
                    DateOfCreation = DateTime.Now,
                    Products = productIds
                    .Select(id => new OrderProduct()
                    {
                        ProductId = id
                    }).ToList()
                }; ;
                db.Add(order);
                db.SaveChanges();
            }
        }

        
    }
}
