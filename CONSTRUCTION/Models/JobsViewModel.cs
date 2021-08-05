using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}