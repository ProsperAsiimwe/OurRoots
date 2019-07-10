using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.Products
{
    public class SearchProductModel : ListModel
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Last name")]
        public string Description { get; set; }



        public bool IsEmpty()
        {
            if (!String.IsNullOrEmpty(this.ProductName) || !String.IsNullOrEmpty(this.Description))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}