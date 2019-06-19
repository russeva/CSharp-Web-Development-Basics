namespace WebServerV._2.ByTheCakeApplication.Services
{
    using System.Collections.Generic;

    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<int> productIds);
        
    }
}
