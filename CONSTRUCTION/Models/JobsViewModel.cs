using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Models
{
    public class JobsViewModel
    {
        public JobsViewModel()
        {
            jobDetailViewModels = new List<JobDetailViewModel>();
        }
        public int currentPage { get; set; }
        public int totalPage { get; set; }
        public int totalRecord { get; set; }
        public string fromtojob { get; set; }
        public List<JobDetailViewModel> jobDetailViewModels { get; set; }
    }

    public class JobDetailViewModel
    {
        public int Id { get; set; }
        public int TechnologyId { get; set; }
        public string Title { get; set; }
        public string TechnologyTitle { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int minExpirience { get; set; }
        public int maxExpirience { get; set; }
        public int minSalary { get; set; }
        public int maxSalary { get; set; }
        public string description { get; set; }
        public string Date { get; set; }
    }

    public class SEOModel
    {
        public string FocusKeyphrase { get; set; }
        public string SEOtitle { get; set; }
        public string H1 { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public bool IsCrawl { get; set; }
        public string IsCrawlString { get; set; }
        public string CanonicalURL { get; set; }
        public string SchemaTags { get; set; }
        [AllowHtml]
        public string jobDescription { get; set; }
        public int cityId { get; set; }
        public int Id { get; set; }
    }

    public class SchemaTagModel
    {
        public string Description { get; set; }
    }
}