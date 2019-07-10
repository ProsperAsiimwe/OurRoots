using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.SupportRequests
{
    public class SupportRequestViewModel
    {

        public SupportRequestViewModel() { }

        public SupportRequestViewModel(SupportRequest SupportRequest)
        {

            SetFromSupportRequest(SupportRequest);
        }

        public int SupportRequestId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Organization")]
        [StringLength(100)]
        public string Organization { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product / Service")]
        [StringLength(100)]
        public string ProductOrService { get; set; }


        public SupportRequest ParseAsEntity(SupportRequest SupportRequest)
        {
            if (SupportRequest == null)
            {
                SupportRequest = new SupportRequest();
            }

            SupportRequest.Name = Name;
            SupportRequest.Organization = Organization;
            SupportRequest.Email = Email;
            SupportRequest.Phone = Phone;
            SupportRequest.ProductOrService = ProductOrService;
            SupportRequest.Description = Description;

            return SupportRequest;
        }

        protected void SetFromSupportRequest(SupportRequest SupportRequest)
        {
            this.SupportRequestId = SupportRequest.SupportRequestId;
            this.Name = SupportRequest.Name;
            this.Organization = SupportRequest.Organization;
            this.Email = SupportRequest.Email;
            this.Phone = SupportRequest.Phone;
            this.ProductOrService = SupportRequest.ProductOrService;
            this.Description = SupportRequest.Description;
        }

    }
}