namespace WebServerV._2.ByTheCakeApplication.Data.Models
{
    using System;
    using System.Collections.Generic;
    public class OrderProduct
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
