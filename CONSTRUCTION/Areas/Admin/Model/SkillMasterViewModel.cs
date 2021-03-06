using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Model
{
    public class SkillMasterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isactive { get; set; }
    }

    public class BlogCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isactive { get; set; }
    }

    public class BlogViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public string BlogTitle { get; set; }
        public string SEOTitle { get; set; }
        public string ShortDescription { get; set; }
        public string SEODescription { get; set; }
        public string Blogdetail { get; set; }
        public string URL { get; set; }
        public string MetaTags { get; set; }
        public string ConicalURL { get; set; }
        public bool isactive { get; set; }
        public bool IsCrawl { get; set; }
    }
}