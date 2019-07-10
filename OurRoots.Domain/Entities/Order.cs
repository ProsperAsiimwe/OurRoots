using OurRoots.Domain.Enums;
using OurRoots.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurRoots.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
            Date = UgandaDateTime.DateNow();
            Status = Status.Pending;
        }

        [Key]
        public int OrderId { get; set; }
        
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(80)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        [StringLength(50)]
        public string Line1 { get; set; }

        [StringLength(50)]
        public string Line2 { get; set; }

        [StringLength(50)]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Zip { get; set; }

        [Display(Name = "Status")]       
        public Status Status { get; set; }

        //public double TotalCost { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }


        [Display(Name = "Total Order Cost (USD)")]
        [NotMapped]
        public double Cost
        {
            get
            {
                return Items.ToList().Sum(p => p.Cost);
            }
        }

        public string GetStatus()
        {
            return Status.ToString();
        }

        public string GetStatusCssClass()
        {
            switch (Status)
            {
                case Status.Pending:
                    return "warning";
                case Status.Delivered:
                    return "success";
                case Status.Dispatched:
                    return "info";
                case Status.Returned:
                    return "danger";
                default:
                    return "primary";
            }

        }

        public string FormatAddress(string concatonator)
        {
            var addr = new List<string>();

            if (!string.IsNullOrEmpty(Line1))
            {
                addr.Add(Line1);
            }
            if (!string.IsNullOrEmpty(Line2))
            {
                addr.Add(Line2);
            }
            if (!string.IsNullOrEmpty(Line3))
            {
                addr.Add(Line3);
            }
            if (!string.IsNullOrEmpty(City))
            {
                addr.Add(City);
            }
            if (!string.IsNullOrEmpty(Zip))
            {
                addr.Add(Zip);
            }
            return string.Join(concatonator, addr.ToArray());
        }


    }
}
