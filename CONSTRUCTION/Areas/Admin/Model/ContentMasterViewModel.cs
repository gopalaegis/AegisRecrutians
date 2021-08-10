using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CONSTRUCTION.Areas.Admin.Model
{
    public class ContentMasterViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string ActionResult { get; set; }
        public string Image { get; set; }
        public string FocusKeyphrase { get; set; }
        public string SEOtitle { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public bool IsCrawl { get; set; }
        public string CanonicalURL { get; set; }
        public string SchemaTags { get; set; }
        public bool isactive { get; set; }
    }
}