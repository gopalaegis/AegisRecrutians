using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Models
{
    public class ApplyJobViewModel
    {
        public ApplyJobViewModel()
        {
            Collage = new List<string>();
            PassingYear = new List<string>();
            StateIdList = new List<int>();
            CountryIdList = new List<int>();
            CityList = new List<string>();
            EmployerList = new List<string>();
            JobTitleList = new List<string>();
            YearofActiveList = new List<string>();
            drpDegree = new List<SelectListItem>();
            drpCountry = new List<SelectListItem>();
            drpState = new List<SelectListItem>();
            educationModel = new List<EducationViewModel>();
            experienceModel = new List<ExperienceViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string AddressType { get; set; }
        public string BlockNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string Degree { get; set; }
        public List<string> Collage { get; set; }
        public List<string> PassingYear { get; set; }
        public List<int> StateIdList { get; set; }
        public List<int> CountryIdList { get; set; }
        public List<string> CityList { get; set; }
        public List<string> EmployerList { get; set; }
        public List<string> JobTitleList { get; set; }
        public List<string> YearofActiveList { get; set; }
        public string Resume { get; set; }
        public int JobId { get; set; }
        public string Job { get; set; }
        public List<SelectListItem> drpDegree { get; set; }
        public List<SelectListItem> drpCountry { get; set; }
        public List<SelectListItem> drpState { get; set; }
        public string jsonEducation { get; set; }
        public string jsonExperience { get; set; }
        public List<EducationViewModel> educationModel { get; set; }
        public List<ExperienceViewModel> experienceModel { get; set; }
    }

    public class EducationViewModel
    {
        public int DegreeId { get; set; }
        public string Degree { get; set; }
        public string Collage { get; set; }
        public int PassingYear { get; set; }

    }

    public class ExperienceViewModel
    {
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        public int ActiveYear { get; set; }
        public string City { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public string State1 { get; set; }
        public string Country1 { get; set; }

    }
}