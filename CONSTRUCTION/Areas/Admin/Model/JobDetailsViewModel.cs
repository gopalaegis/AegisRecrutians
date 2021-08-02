using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Model
{
    public class JobDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public List<SelectListItem> SubCategoryList { get; set; }
        public string Profession { get; set; }
        public List<SelectListItem> ProfessionList { get; set; }
        public int professionId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int MinExperience { get; set; }
        public int MaxExperience { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public string BriefDescription { get; set; }
        public string JobEducation { get; set; }
        public string JobQualification { get; set; }
        public Boolean IsVerified { get; set; }
        public string ShowOnHome { get; set; }
    }
}