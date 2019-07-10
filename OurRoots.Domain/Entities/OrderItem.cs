using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurRoots.Domain.Entities
{
   public class OrderItem
    {

        [Key]
        public int OrderItemId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }       

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }

        [NotMapped]
        public double Cost
        {
            get
            {
                return (Product.Price * Quantity);
            }
        }

    }
}
