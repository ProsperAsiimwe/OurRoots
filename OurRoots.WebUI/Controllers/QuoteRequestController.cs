using OurRoots.Domain.Context;
using OurRoots.Domain.Entities;
using OurRoots.WebUI.Models.QuoteRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurRoots.WebUI.Controllers
{
    public class QuoteRequestController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: QuoteRequest
        public ActionResult Index()
        {
            var model = new QuoteRequestListViewModel
            {
                QuoteRequests = ctx.QuoteRequests.ToList()
            };
            return View(model);
        }

       
    }
}