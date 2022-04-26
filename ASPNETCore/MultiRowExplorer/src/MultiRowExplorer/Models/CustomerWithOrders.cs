using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiRowExplorer.Models
{
    public class CustomerWithOrders : Customer
    {
        public List<Order> Orders { get; set; }
    }
}