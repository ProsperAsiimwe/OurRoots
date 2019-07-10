using OurRoots.Domain.Context;
using OurRoots.WebUI.Models.SupportRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurRoots.WebUI.Controllers
{
    public class SupportRequestController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: SupportRequest
        public ActionResult Index()
        {
            var model = new SupportRequestListViewModel
            {
                SupportRequests = ctx.SupportRequests.ToList()
            };
            return View(model);
        }
    }
}