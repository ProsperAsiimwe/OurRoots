using System;
using System.Collections.Generic;
using MagicApps.Infrastructure.Services;
using MagicApps.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TwitterBootstrap3;
using OurRoots.Domain.Context;
using OurRoots.Domain.Entities;
using OurRoots.WebUI.Models.Products;

namespace OurRoots.WebUI.Infrastructure.Helpers
{
    public class ProductHelper
    {
        private ApplicationDbContext db;
        private ApplicationUserManager UserManager;

        int productId;

        public Product Product { get; private set; }

        public string ServiceUserId { get; set; }

        public ProductHelper()
        {
            Set();
        }

        public ProductHelper(int productId)
        {
            Set();

            this.productId = productId;
            this.Product = db.Products.Find(productId);
        }

        public ProductHelper(Product Product)
        {
            Set();

            this.productId = Product.productId;
            this.Product = Product;
        }

        private void Set()
        {
            this.db = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            this.UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public ProductListViewModel GetProductList(SearchProductModel searchModel, int page = 1)
        {
            int pageSize = 20;

            if (page < 1)
            {
                page = 1;
            }

            IEnumerable<Product> records = db.Products.ToList();

            if (!String.IsNullOrEmpty(searchModel.ProductName))
            {
                string name = searchModel.ProductName.ToLower();
                records = records.Where(x => x.productName.ToLower().Contains(name));
            }
            if (!String.IsNullOrEmpty(searchModel.Description))
            {
                string name = searchModel.Description.ToLower();
                records = records.Where(x => x.description.ToLower().Contains(name));
            }

            //if (!String.IsNullOrEmpty(searchModel.Status))
            //{
            //    records = records.Where(p => p.GetStatus() == searchModel.Status);
            //}

            //if (searchModel.Status != "Terminated")
            //{
            //    records = records.Where(p => !p.Terminated.HasValue);
            //}

            return new ProductListViewModel
            {
                //Products = records
                //    .OrderByDescending(o => o.UploadDate)
                //    .Skip((page - 1) * pageSize)
                //    .Take(pageSize),
                //SearchModel = searchModel,
                //PagingInfo = new PagingInfo
                //{
                //    CurrentPage = page,
                //    PageSize = pageSize,
                //    TotalItems = records.Count()
                //}
            };
        }

        public async Task<UpsertModel> UpsertProduct(UpsertMode mode, ProductViewModel model)
        {
            var upsert = new UpsertModel();

            //try
            //{
            //Activity activity;

            string title;
            System.Text.StringBuilder builder;

            // Apply changes
            Product = model.ParseAsEntity(Product);

            builder = new System.Text.StringBuilder();

            if (model.productId == 0)
            {
                db.Products.Add(Product);

                title = "Product Created";
                builder.Append("A Product record has been created for:").AppendLine();
            }
            else
            {
                db.Entry(Product).State = System.Data.Entity.EntityState.Modified;

                title = "Product Updated";
                builder.Append("Changes have been made to the Product details.");

                if (mode == UpsertMode.Admin)
                {
                    builder.Append(" (by the Admin)");
                }

                builder.Append(":").AppendLine();
            }

            await db.SaveChangesAsync();

            productId = Product.productId;

            if (model.ProductImage != null)
            {
                UploadImage(model.ProductImage);
            }

            //if (!(model.Evidence == null || model.Evidence.Length == 0 || model.Evidence[0] == null))
            //{
            //    UploadEvidence(model.Evidence);
            //}

            // Save activity now so we have a productId. Not ideal, but hey

            //activity = CreateActivity(title, builder.ToString());
            //activity.UserId = ServiceUserId;

            await db.SaveChangesAsync();

            if (model.productId == 0)
            {
                upsert.ErrorMsg = "Product record created successfully";
            }
            else
            {
                upsert.ErrorMsg = "Product record updated successfully";
            }

            upsert.RecordId = Product.productId.ToString();
            //}
            //catch (Exception ex)
            //{
            //    upsert.ErrorMsg = ex.Message;
            //}

            return upsert;
        }



        public async Task<UpsertModel> DeleteProduct()
        {
            var upsert = new UpsertModel();

            //try
            //{
            string title = "Product Deleted";
            System.Text.StringBuilder builder = new System.Text.StringBuilder()
                .Append("The following Product has been deleted:")
                .AppendLine()
                .AppendLine().AppendFormat("Product Name: {0}", Product.productName)
                .AppendLine().AppendFormat("Description: {0}", Product.description)

                .AppendLine().AppendFormat("Added: {0}", Product.UploadDate.ToString("ddd, dd MMM yyyy"));

            // Record activity

            //var activity = CreateActivity(title, builder.ToString());
            //activity.UserId = ServiceUserId;

            // Remove Product
            // this.Product.Terminated = UgandaDateTime.DateNow();
            db.Products.Remove(Product);
            db.Entry(Product).State = System.Data.Entity.EntityState.Modified;
            DeletePic();

            await db.SaveChangesAsync();

            upsert.ErrorMsg = string.Format("Product: '{0}' terminated successfully", Product.productName);
            upsert.RecordId = Product.productId.ToString();
            //}
            //catch (Exception ex)
            //{
            //    upsert.ErrorMsg = ex.Message;
            //}

            return upsert;
        }


        //public Activity CreateActivity(string title, string description)
        //{
        //    var activity = new Activity
        //    {
        //        Title = title,
        //        Description = description,
        //        RecordedById = ServiceUserId,
        //        productId = productId
        //    };
        //    db.Activities.Add(activity);
        //    return activity;
        //}

        public static ButtonStyle GetButtonStyle(string css)
        {
            ButtonStyle button_css;

            if (css == "warning")
            {
                button_css = ButtonStyle.Warning;
            }
            else if (css == "success")
            {
                button_css = ButtonStyle.Success;
            }
            else if (css == "info")
            {
                button_css = ButtonStyle.Info;
            }
            else
            {
                button_css = ButtonStyle.Danger;
            }

            return button_css;
        }

        //private void RecordException(string title, Exception ex)
        //{
        //    var activity = CreateActivity(title, ex.Message);

        //    if (Product != null)
        //    {
        //        activity.UserId = ServiceUserId;
        //        activity.productId = Product.productId;
        //    }
        //    db.SaveChanges();
        //}

        private bool UploadImage(HttpPostedFileBase file)
        {
            //try
            //{

                if (file != null)
                {
                    //string folder = @"~/Content/ProductImages";
                    //FileService.CreateFolder(folder);

                    //var fileExt = Path.GetExtension((file as HttpPostedFileBase).FileName).Substring(1);
                    //var fileName = string.Format("{0}.{1}", productId, fileExt);
                    //folder = ConfigurationManager.AppSettings["Settings.Site.ImgFolder"];


                    //string imageFolder = "~/Content/ProductImages";
                    //string imageFolderPath = Path.Combine(imageFolder, fileName);



                    //string path = Path.Combine(folder, fileName);

                    // delete the file if it exists
                    //FileService.DeleteFile(path);
                    //file.SaveAs(path);


                    //Product.FileName = fileName;
                    //Product.FolderPath = imageFolderPath;

                    //db.Entry(Product).State = System.Data.Entity.EntityState.Modified;



                    string folder = @"~/Content/ProductImages";
                    FileService.CreateFolder(folder);

                    string fileName = Path.GetFileNameWithoutExtension((file as HttpPostedFileBase).FileName);
                    string extension = Path.GetExtension((file as HttpPostedFileBase).FileName).Substring(1);
                    folder = ConfigurationManager.AppSettings["Settings.Site.ImgFolder"];
                    fileName = string.Format("{0}.{1}", productId, extension);
                    string path = Path.Combine(folder, fileName);
                    string dbPath = Path.Combine(folder, Product.FileName);

                    FileService.DeleteFile(path);                
                    FileService.DeleteFile(dbPath);

                    file.SaveAs(path);
                    string imageFolder = "~/Content/ProductImages";
                    string imageFolderPath = Path.Combine(imageFolder, fileName);

                    Product.FileName = fileName;
                    //Product.FolderPath = imageFolderPath;
                    db.Entry(Product).State = System.Data.Entity.EntityState.Modified;
                  

                }
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}


            return true;
        }

        private bool DeletePic()
        {
            //try
            //{
            var Pic = string.Format(@"~/content/productimages/{0}", productId);
            var folder = ConfigurationManager.AppSettings["Settings.Site.ImgFolder"];
            folder = string.Format(@"~/content/productimages", folder);

            string path = Path.Combine(folder, string.Format("{0}", Pic));

            FileService.DeleteFile(path);
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}


            return true;
        }
    }
}