using CONSTRUCTION.CommonMethods;
using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Controllers
{
    public class AppliedJobsController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        [UserLoginCheck]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AppliedJobList()
        {
            List<ApplyJobViewModel> model = new List<ApplyJobViewModel>();
            var data = _db.tblApplyJobDetails.ToList();
            foreach (var item in data)
            {
                var j = _db.tblJobDetails.Where(y => y.Id == item.JobId).FirstOrDefault();
                ApplyJobViewModel m = new ApplyJobViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    DOB = (Convert.ToDateTime(item.DOB)).ToString("MMM dd, yyyy"),
                    Gender = item.Gender,
                    Email = item.Email,
                    PhoneNo1 = item.Mobile,
                    Job = j != null ? j.Title : ""
                };
                model.Add(m);
            }
            return PartialView("_partialAppliedJobList", model);
        }

        public ActionResult AddEditAppliedJob(int Id = 0)
        {
            ApplyJobViewModel model = new ApplyJobViewModel();
            if (Id > 0)
            {
                var data = _db.tblApplyJobDetails.Where(x => x.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    var j = _db.tblJobDetails.Where(y => y.Id == data.JobId).FirstOrDefault();
                    var country = _db.tblCountries.Where(y => y.Id == data.CountryId).FirstOrDefault();
                    var state = _db.tblStates.Where(y => y.Id == data.StateId).FirstOrDefault();

                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.DOB = (Convert.ToDateTime(data.DOB)).ToString("MMM dd, yyyy");
                    model.Gender = data.Gender;
                    model.Email = data.Email;
                    model.PhoneNo1 = data.Mobile;
                    model.PhoneNo2 = data.Mobile1;
                    model.Job = j != null ? j.Title : "";
                    model.AddressType = data.AddressType;
                    model.BlockNo = data.HouseName;
                    model.Street = data.Street;
                    model.City = data.City;
                    model.State = state.Name;
                    model.Country = country.Name;

                    var education = _db.tblApplyJobEducations.Where(x => x.ApplyJobDetailId == Id).ToList();
                    foreach (var item in education)
                    {
                        var d = _db.tblJobEducations.Where(y => y.Id == item.DegreeId).FirstOrDefault();
                        if (d != null)
                        {
                            EducationViewModel e = new EducationViewModel();
                            e.Degree = d.Education;
                            e.Collage = item.Collage;
                            e.PassingYear = (int)item.PassingYear;
                            model.educationModel.Add(e);
                        }
                    }

                    var experience = _db.tblApplyJobExperiences.Where(x => x.ApplyJobDetailId == Id).ToList();
                    foreach (var item in experience)
                    {
                        var country1 = _db.tblCountries.Where(y => y.Id == item.CountryId).FirstOrDefault();
                        var state1 = _db.tblStates.Where(y => y.Id == item.StateId).FirstOrDefault();
                        if (country1 != null&&state1!=null)
                        {
                            ExperienceViewModel e = new ExperienceViewModel();
                            e.Employer = item.Employer;
                            e.JobTitle = item.JobTitle;
                            e.ActiveYear = (int)item.ActiveYear;
                            e.City = item.City;
                            e.State1 = state1.Name;
                            e.Country1 = country1.Name;
                            model.experienceModel.Add(e);
                        }
                    }
                }
            }
            return PartialView("_partialAppliedJobDetail", model);
        }
    }
}