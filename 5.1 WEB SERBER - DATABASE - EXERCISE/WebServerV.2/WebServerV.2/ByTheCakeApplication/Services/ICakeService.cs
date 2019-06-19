namespace WebServerV._2.ByTheCakeApplication.Services
{
    using System.Collections;
    using System.Collections.Generic;
    using ByTheCakeApplication.ViewModels.Cake;

    public interface ICakeService
    {
        void Create(string name, decimal price, string imageUrl);

        IEnumerable<CakeListingViewModel> All(string searchTerm = null);

        CakeDetailsViewModel Find(int id);
        
        bool Exists(int id);

        IEnumerable<CakeInCartViewModel> FindProductsInCart(IEnumerable<int> ids);
    }
}
