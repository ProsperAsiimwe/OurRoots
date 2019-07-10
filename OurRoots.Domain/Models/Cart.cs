using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurRoots.Domain.Models
{
    public class Cart
    {
        private List<CartLine> itemsCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = itemsCollection.Where(p => p.Product.productId == product.productId).FirstOrDefault();
            if (line == null)
            {
                itemsCollection.Add(new CartLine { Product = product, Quantity = quantity, Discount = 0 });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveItem(Product product)
        {
            itemsCollection.RemoveAll(p => p.Product.productId == product.productId);
        }

        public double computeTotalValue()
        {
            return itemsCollection.Sum(p => p.Cost);
        }

        public IEnumerable<CartLine> Lines
        {
            get { return itemsCollection; }
        }

        public void Clear()
        {
            itemsCollection.Clear();
        }
    }
}
