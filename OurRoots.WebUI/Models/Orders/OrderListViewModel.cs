using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.Orders
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}