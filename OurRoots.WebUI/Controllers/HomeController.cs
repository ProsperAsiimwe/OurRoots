using OurRoots.Domain.Context;
using OurRoots.Domain.Entities;
using OurRoots.WebUI.Infrastructure.Helpers;
using OurRoots.WebUI.Models.ContactMessages;
using OurRoots.WebUI.Models.GalleryUploads;
using OurRoots.WebUI.Models.Products;
using OurRoots.WebUI.Models.QuoteRequests;
using OurRoots.WebUI.Models.SupportRequests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurRoots.WebUI.Controllers
{
    public class HomeController : BaseController
    {
       

        public ActionResult Index()
        {
            ViewBag.Active = "Home";
            return View();
        }

      
        public ActionResult About()
        {
            ViewBag.Active = "About";
            return View();

        }

        public ActionResult Gallery()
        {
            ViewBag.Active = "Gallery";

            var model = new GalleryListViewModel
            {
                GalleyUploads = context.GalleryUploads.ToList()
            };
            return View(model);
        }

        public ActionResult Events()
        {

            return View();
        }

        public ActionResult Shop()
        {
            ViewBag.Active = "Shop";

            var model = new ProductListViewModel
            {
                Products = context.Products.ToList()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Active = "Contact";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               
                    var msg = viewModel.ParseAsEntity(new ContactMessage());
                    context.ContactMessages.Add(msg);
                    context.SaveChanges();

                    // send email
                    var mail = GetMailHelper();
                    string subject = string.Format("{0} - Contact Message", msg.Name);
                    string message = ContactMsgNotification(msg);
                    string status = string.Join(":", mail.SendMail(subject, message, ConfigurationManager.AppSettings["Settings.Company.Email"]));
                    mail.RecordErrors();

                    return RedirectToAction("Thanks");
               
            }

            return View();
        }


        public ActionResult Quotation()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Quotation(QuoteRequestViewModel q)
        {
            if (ModelState.IsValid)
            {
                using (context)
                {
                    var quote = q.ParseAsEntity(new QuoteRequest());
                    context.QuoteRequests.Add(quote);
                    context.SaveChanges();

                    // send email
                    var mail = GetMailHelper();
                    string subject = string.Format("{0} - Quote request", quote.Name);
                    string message = QuotationNotificationMsg(quote);
                    string status = string.Join(":", mail.SendMail(subject, message, ConfigurationManager.AppSettings["Settings.Company.Email"]));
                    mail.RecordErrors();

                    return RedirectToAction("Thanks");
                }

            }

            return View();
        }

        public ActionResult Support()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Support(SupportRequestViewModel s)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (context)
        //        {
        //            var support = s.ParseAsEntity(new SupportRequest());
        //            context.SupportRequests.Add(support);
        //            context.SaveChanges();

        //            // send email
        //            var mail = GetMailHelper();
        //            string subject = string.Format("{0} - Support request", support.Name);
        //            //string message = SupportNotificationMsg(support);
        //            string status = string.Join(":", mail.SendMail(subject, message, ConfigurationManager.AppSettings["Settings.Company.Email"]));
        //            mail.RecordErrors();

        //            return RedirectToAction("Thanks");
        //        }

        //    }

        //    return View();
        //}

        public ActionResult AdminPage()
        {

            return RedirectToAction("Index", "HomeAdmin");
        }

        public ActionResult Thanks()
        {
            return View();
        }

        public MailHelper GetMailHelper()
        {
            MailHelper mail = new MailHelper(null);
            mail.UserId = null;

            return mail;
        }

    }
}