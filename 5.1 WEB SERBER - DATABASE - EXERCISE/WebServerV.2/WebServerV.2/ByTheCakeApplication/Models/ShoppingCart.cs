namespace WebServerV._2.ByTheCakeApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public  class ShoppingCart
    {
        public const string SessionKey = "%^Current_Shopping_Cart^%";

        public  List<int> OrderIds { get; private set; } = new List<int>(); 
    }
}
