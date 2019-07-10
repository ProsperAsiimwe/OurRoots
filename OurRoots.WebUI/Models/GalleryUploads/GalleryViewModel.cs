using OurRoots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurRoots.WebUI.Models.GalleryUploads
{
    public class GalleryViewModel
    {

        public GalleryViewModel() { }

        public GalleryViewModel(GalleryUpload GalleryUpload)
        {
            setFromGalleryUpload(GalleryUpload);
        }

        [Key]
        public int GalleryUploadId { get; set; }

        [Required(ErrorMessage = "Gallery Image is Required")]
        [Display(Name = "Gallery Image")]

        public HttpPostedFileBase[] GalleryImages { get; set; }

        public GalleryUpload ParseAsEntity(GalleryUpload GalleryUpload)
        {
            if (GalleryUpload == null)
            {
                GalleryUpload = new GalleryUpload();
            }
                    
            return GalleryUpload;

        }



        public void setFromGalleryUpload(GalleryUpload GalleryUpload)
        {
            this.GalleryUploadId = GalleryUpload.GalleryUploadId;
          
        }

    }
}