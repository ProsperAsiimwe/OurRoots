using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.Orders
{
    public class OrderViewModel
    {
        public OrderViewModel() { }

        public OrderViewModel(Order Order)
        {
            setFromOrder(Order);
        }

        [Key]
        public int OrderId { get; set; }
        
        [Required(ErrorMessage = "Please enter a name")]
        [Display(Name = "Name")]
        [StringLength(80)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Display(Name = "Phone")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        [Display(Name = "Address 1")]
        [StringLength(50)]
        public string Line1 { get; set; }

        [Display(Name = "Address 2")]
        [StringLength(50)]
        public string Line2 { get; set; }

        [Display(Name = "Address 3")]
        [StringLength(50)]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        [Display(Name = "City")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Zip code")]
        [StringLength(50)]
        public string Zip { get; set; }


        public Order ParseAsEntity(Order Order)
        {
            if (Order == null)
            {
                Order = new Order();
            }

            Order.Name = Name;
            Order.Email = Email;
            Order.Phone = Phone;
            Order.Line1 = Line1;
            Order.Line2 = Line2;
            Order.Line3 = Line3;
            Order.City = City;
            Order.Zip = Zip;


            return Order;

        }

        public void setFromOrder(Order Order)
        {
            this.Name = Order.Name;
            this.Email = Order.Email;
            this.Phone = Order.Phone;
            this.Line1 = Order.Line1;
            this.Line2 = Order.Line2;
            this.Line3 = Order.Line3;
            this.City = Order.City;
            this.Zip = Order.Zip;
        }

    }
}