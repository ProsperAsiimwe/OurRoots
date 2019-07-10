using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.Products
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}