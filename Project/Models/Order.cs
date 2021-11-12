using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Order
    {
        public int id { get; set; }
        public int uid { get; set; }

        public int pid { get; set; }
        
        public decimal price { get; set; }
        public int quantity { get; set; }
        public double total { get; set; }

        public string status { get; set; }
        public DateTime time { get; set; }
        [NotMapped]
        public string productname { get; set; }
    }
}