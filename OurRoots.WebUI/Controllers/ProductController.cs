using MagicApps.Infrastructure.Services;
using OurRoots.Domain.Context;
using OurRoots.Domain.Entities;
using OurRoots.WebUI.Infrastructure;
using OurRoots.WebUI.Infrastructure.Helpers;
using OurRoots.WebUI.Models.Products;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OurRoots.WebUI.Controllers
{
    public class ProductController : BaseController
    {
      
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Active = "Product";

            var model = new ProductListViewModel
            {
                Products = context.Products.ToList()
            };
            return View(model);
        }

        public ActionResult New()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
             
                    var item = viewModel.ParseAsEntity(new Product());
                    context.Products.Add(item);
                    context.SaveChanges();

                    string folder = @"~/content/productimages";
                    FileService.CreateFolder(folder);

                    string fileName = Path.GetFileNameWithoutExtension(viewModel.ProductImage.FileName);
                    string extension = Path.GetExtension(viewModel.ProductImage.FileName).Substring(1);
                    folder = ConfigurationManager.AppSettings["Settings.Site.ImgFolder"];
                    fileName = string.Format("{0}.{1}", item.productId, extension);
                    string path = Path.Combine(folder, fileName);

                FileService.DeleteFile(path);
                viewModel.ProductImage.SaveAs(path);
                string imageFolder = "~/content/productimages";
                string imageFolderPath = Path.Combine(imageFolder, fileName);

                item.FileName = fileName;
                //item.FolderPath = imageFolderPath;
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = context.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product products = context.Products.Find(id);
            context.Products.Remove(products);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (!IsRoutingOK(id))
            {
                return RedirectOnError();
            }

            var model = GetProductModel(id);

            //return View("New", model);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int productId, ProductViewModel model)
        {
            if (!IsRoutingOK(productId))
            {
                return RedirectOnError();
            }
            bool success =  await Upsert(productId, model);
            if (success)
            {
                return RedirectOnSuccess(productId);
            }

            //return View("New", model);

            return View(model);

        }


        //[HttpPost]
        //public ActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Entry(product).State = EntityState.Modified;
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(product);
        //}

        private async Task<bool> Upsert(int? ProductId, ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var helper = (ProductId.HasValue ? GetHelper(ProductId.Value) : new ProductHelper() { ServiceUserId = GetUserId() });
                var upsert = await helper.UpsertProduct(UpsertMode.Admin, model);

                if (upsert.i_RecordId() > 0)
                {
                    ShowSuccess(upsert.ErrorMsg);

                    return true;
                }
                else
                {
                    ShowError(upsert.ErrorMsg);
                }
            }

            //model.SetLists();
            return false;
        }

        private ProductHelper GetHelper(int ProductId)
        {
            ProductHelper helper = new ProductHelper(ProductId);

            helper.ServiceUserId = GetUserId();

            return helper;
        }



        private RedirectToRouteResult RedirectOnError()
        {
            return RedirectToAction("Index");
        }

        private RedirectToRouteResult RedirectOnSuccess(int productId)
        {
            return RedirectToAction("Index", new { productId = productId});
        }


        private bool IsRoutingOK(int ? productId)
        {
            if (productId.HasValue)
            {
                var Product = context.Products.ToList().SingleOrDefault(x => x.productId == productId);
                if (Product == null)
                {
                    return false;
                }
            }

            return true;
        }

        private ProductViewModel GetProductModel(int? productId)
        {
            ProductViewModel model;
            if (productId.HasValue)
            {
                var Product = GetProduct(productId.Value);
                model = new ProductViewModel(Product);
            }else
            {
                model = new ProductViewModel();
            }

            return model;
        }

        private Product GetProduct(int productId)
        {
            return context.Products.Find(productId);
        }

    }
}