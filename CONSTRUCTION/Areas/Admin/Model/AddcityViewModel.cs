using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Model
{
    public class AddcityViewModel
    {
        public AddcityViewModel()
        {
            drpcity = new List<SelectListItem>();
        }
        public int id { get; set; }
        public int teckmasterId { get; set; }
        public string teckmaster { get; set; }
        public string FocusKeyphrase { get; set; }
        public string SEOtitle { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public string BriefDescription { get; set; }
        public bool IsCrawl { get; set; }
        public string CanonicalURL { get; set; }
        public string SchemaTags { get; set; }
        public bool isactive { get; set; }
        public string city { get; set; }
        public int cityId { get; set; }
        public List<SelectListItem> drpcity { get; set; }
    }
}