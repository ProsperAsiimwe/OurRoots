using OurRoots.Domain.Abstract;
using OurRoots.Domain.Concrete;
using OurRoots.Domain.Entities;
using OurRoots.Domain.Models;
using OurRoots.WebUI.Infrastructure.Helpers;
using OurRoots.WebUI.Models;
using OurRoots.WebUI.Models.Orders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OurRoots.WebUI.Controllers
{
    [RoutePrefix("Cart")]
    public class CartController : BaseController
    {
        public CartController()
        {
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(GetCart());
        }

        public ViewResult Checkout()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(OrderViewModel model)
        {
            if (GetCart().Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            //if (ModelState.IsValid)
            //{
            //    // save sale and create sale items 
            //    var helper = new SaleHelper();
            //    var result = await helper.UpsertSale(Infrastructure.UpsertMode.Admin, model, GetCart());
            //    if (result.i_RecordId() > 0)
            //    {
            //        GetCart().Clear();
            //    }

            //    return RedirectToAction("Index", "Sales");
            //}


            if (ModelState.IsValid)
            {

                var order = model.ParseAsEntity(new Order());
                context.Orders.Add(order);
                context.SaveChanges();

                var cart = GetCart();
                
                if (order.OrderId > 0)
                {
                    if (cart.Lines.Count() > 0)
                    {
                        var items = cart.Lines.Select(p => new OrderItem
                        {
                            OrderId = order.OrderId,
                            ProductId = p.Product.productId,
                            Quantity = p.Quantity
                        });

                        context.OrderItems.AddRange(items);
                        context.SaveChanges();

                    }

                    //order.TotalCost = order.Cost;

                    // clear cart
                    GetCart().Clear();
                }

                // send email

                var mail = GetMailHelper();
                string subject = string.Format("{0} - New Order", order.OrderId);
                string message = OrdersNotificationMsg(order);
                string status = string.Join(":", mail.SendMail(subject, message, ConfigurationManager.AppSettings["Settings.Company.Email"]));
                mail.RecordErrors();


                return RedirectToAction("Thanks", "Home");

            }
          
                return View();
          

        }

        public RedirectToRouteResult AddToCart(int productId, int quantity, string returnUrl)
        {
            Product product = context.Products.FirstOrDefault(p => p.productId == productId);
            //var CurrQty = GetUser().Branch.GetPdtStock(product.ProductId);
            //if (CurrQty < quantity)
            //{
            //    ShowError("Quantity selected is below your Branch's stock level.");
            //    return RedirectToAction("Index", "Products");
            //}
            if (product != null)
            {
                GetCart().AddItem(product, quantity > 0 ? quantity : 1);
            }
            return RedirectToAction("Shop", "Home");
        }
        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = context.Products.FirstOrDefault(p => p.productId == productId);

            if (product != null)
            {
                GetCart().RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public MailHelper GetMailHelper()
        {
            MailHelper mail = new MailHelper(null);
            mail.UserId = null;

            return mail;
        }


        [Route("{ProductId:int}/{quantity:int}")]
        public JsonResult UpdateCost(int productId, int quantity)
        {
            var product = GetCart().Lines.FirstOrDefault(p => p.Product.productId == productId);

            if (product != null)
            {
                product.Quantity = quantity;

                return Json(new { cost = string.Format("{0} USD", product.Cost.ToString("n0")), total = string.Format("{0} USD", GetCart().computeTotalValue().ToString("n0")) });
            }

            return null;
        }

        [Route("{ProductId:int}/{discount}/Disc")]
        public JsonResult UpdateDisc(int productId, double discount)
        {

            var product = GetCart().Lines.FirstOrDefault(p => p.Product.productId == productId);

            if (product != null)
            {
                product.Discount = discount;

                return Json(new { cost = string.Format("{0} USD", product.Cost.ToString("n0")), total = string.Format("{0} USD", GetCart().computeTotalValue().ToString("n0")) });
            }

            return null;
        }

        public Product GetProduct(int id)
        {
            var product = context.Products.Find(id);
            return product;
        }
    }
}

