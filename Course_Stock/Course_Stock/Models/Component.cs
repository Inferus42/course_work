using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Stock.Models
{
    public class Component
    {
        public string name { get; set; }
        public int quantityInStockNow { get; set; }
        public int quantityInProduct { get; set; }
        public int quantityInStockAfter { get; set; }


    }
}