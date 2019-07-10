using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.Products
{
    public class ProductViewModel
    {

        public ProductViewModel() { }

        public ProductViewModel(Product Product)
        {
            setFromProduct(Product);
        }

        [Key]
        public int productId { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [Display(Name = "Name")]       
        [StringLength(50)]
        public string productName { get; set; }

        [Required(ErrorMessage = "Product Description is Required")]
        [Display(Name = "Description")]        
        [StringLength(100)]
        public string description { get; set; }

        [Required(ErrorMessage = "Product Price should be a positive price")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        //[Required(ErrorMessage = "Product Category is Required")]
        //[Display(Name = "Category")]
        //[StringLength(50)]
        //public string category { get; set; }

        [Required(ErrorMessage = "Product Image is Required")]
        [Display(Name = "Product Image")]

        public HttpPostedFileBase ProductImage { get; set; }


        public Product ParseAsEntity(Product Product)
        {
            if (Product == null)
            {
                Product = new Product();
            }

            Product.productName = productName;
            Product.description = description;
            Product.Price = Price;
            //Product.category = category;

            return Product;

        }

        public void setFromProduct(Product Product)
        {
            this.productId = Product.productId;
            this.productName = Product.productName;
            this.description = Product.description;
            this.Price = Product.Price;
            //this.category = Product.category;
        }

    }
}