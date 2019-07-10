using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurRoots.Domain.Models
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double Discount { get; set; }

        public double Cost
        {
            get
            {
                return (Product.Price * Quantity) - (Product.Price * Quantity * Discount / 100);
            }
        }
    }
}
