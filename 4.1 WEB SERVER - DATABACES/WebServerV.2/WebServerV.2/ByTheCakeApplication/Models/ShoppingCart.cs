﻿namespace WebServerV._2.ByTheCakeApplication.Models
{
    using System;
    using System.Collections.Generic;

    public  class ShoppingCart
    {
        public const string SessionKey = "%^Current_Shopping_Cart^%";

        public  List<Cake> Orders { get; private set; } = new List<Cake>(); 
    }
}
