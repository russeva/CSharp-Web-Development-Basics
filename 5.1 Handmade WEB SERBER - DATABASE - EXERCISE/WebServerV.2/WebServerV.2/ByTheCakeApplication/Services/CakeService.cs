namespace WebServerV._2.ByTheCakeApplication.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ByTheCakeApplication.Data;
    using ByTheCakeApplication.Data.Models;
    using WebServerV._2.ByTheCakeApplication.Models;
    using WebServerV._2.ByTheCakeApplication.ViewModels.Cake;

    public class CakeService :  ICakeService
    {
        
        public void Create(string name, decimal price, string imageUrl)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = new Product()
                {
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                };

                db.Product.Add(product);
                db.SaveChanges();
            }
            
        }

        public IEnumerable<CakeListingViewModel> All(string searchTerm = null)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var resultQuery = db.Product.AsQueryable();

                if(!string.IsNullOrEmpty(searchTerm))
                {
                    resultQuery = resultQuery
                        .Where(r => r.Name.ToLower().Contains(searchTerm.ToLower()));
                }

                return resultQuery.Select(p => new CakeListingViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();
            }
            
        }

        public CakeDetailsViewModel Find(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Product
                    .Where(p => p.Id == id)
                    .Select(p => new CakeDetailsViewModel()
                    {
                        Name = p.Name,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    })
                    .FirstOrDefault();
            }
         
        }
        
        public bool Exists(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var cake = db.Product.Any(p => p.Id == id);
                return cake;
            }
        }

        public IEnumerable<CakeInCartViewModel> FindProductsInCart(IEnumerable<int> ids)
        {
            using (var db = new ByTheCakeDbContext())
            {
               return db.Product
                    .Where(pr => ids.Contains(pr.Id))
                    .Select(pr => new CakeInCartViewModel()
                    {
                        Name = pr.Name,
                        Price = pr.Price
                    })
                    .ToList();

            }
        }
    }
}
