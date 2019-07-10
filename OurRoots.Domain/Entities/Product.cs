using OurRoots.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurRoots.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            UploadDate = UgandaDateTime.DateNow();
        }

        [Key]        
        public int productId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(50)]
        public string productName { get; set; }

        [Required(ErrorMessage = "Product Description is Required")]
        [StringLength(100)]
        public string description { get; set; }

        [Required(ErrorMessage = "Product Price should be a positive price")]
        public double Price { get; set; }

        //[Required(ErrorMessage = "Product Category is Required")]
        //[StringLength(50)]
        //public string category { get; set; }

        [Display(Name = "Image")]
        [StringLength(1000)]
        public string FileName { get; set; }

        //[Display(Name = "Image")]
        //[StringLength(1000)]
        //public string FolderPath { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
