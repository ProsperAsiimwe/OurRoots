using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.SupportRequests
{
    public class SupportRequestListViewModel
    {
        public IEnumerable<SupportRequest> SupportRequests { get; set; }
    }
}