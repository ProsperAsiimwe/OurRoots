using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.QuoteRequests
{
    public class QuoteRequestViewModel
    {

        public QuoteRequestViewModel() { }

        public QuoteRequestViewModel(QuoteRequest QuoteRequest)
        {

            SetFromQuoteRequest(QuoteRequest);
        }

        public int QuoteRequestId { get; set; }

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
        [Display(Name = "Product / Service")]
        [StringLength(100)]
        public string ProductOrService { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(2000)]
        public string Description { get; set; }


        public QuoteRequest ParseAsEntity(QuoteRequest QuoteRequest)
        {
            if (QuoteRequest == null)
            {
                QuoteRequest = new QuoteRequest();
            }

            QuoteRequest.Name = Name;
            QuoteRequest.Organization = Organization;
            QuoteRequest.Email = Email;
            QuoteRequest.Phone = Phone;
            QuoteRequest.ProductOrService = ProductOrService;
            QuoteRequest.Description = Description;

            return QuoteRequest;
        }

        protected void SetFromQuoteRequest(QuoteRequest QuoteRequest)
        {
            this.QuoteRequestId = QuoteRequest.QuoteRequestId;
            this.Name = QuoteRequest.Name;
            this.Organization = QuoteRequest.Organization;
            this.Email = QuoteRequest.Email;
            this.Phone = QuoteRequest.Phone;
            this.ProductOrService = QuoteRequest.ProductOrService;
            this.Description = QuoteRequest.Description;
        }

    }
}