using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.ContactMessages
{
    public class ContactMessageListViewModel
    {
        public IEnumerable<ContactMessage> ContactMessages { get; set; }
    }
}