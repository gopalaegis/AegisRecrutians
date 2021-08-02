using CONSTRUCTION.Areas.Admin.Model;
using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class HomeController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.skillMasterViewModels = _db.tblSkillMasters.Select(x => new SkillMasterViewModel { Id = x.Id, Name = x.Name }).ToList();
            model.technologyMasterViewModels = _db.tblTechnologyMasters.Select(x => new TechnologyMasterViewModel { Id = x.Id, Name = x.Name, Image = x.Image }).ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult JobDetails()
        {
            List<JobDetailViewModel> model = new List<JobDetailViewModel>();
            var m = _db.tblJobDetails.OrderByDescending(x => x.Id).ToList();
            foreach (var item in m)
            {
                var cityData = _db.tblCities.Where(x => x.Id == item.CityId).FirstOrDefault();
                JobDetailViewModel data = new JobDetailViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    CityId = (int)item.CityId,
                    minExpirience = (int)item.MinExperience,
                    maxExpirience = (int)item.MaxExperience,
                    minSalary = (int)item.MinSalary,
                    maxSalary = (int)item.MaxSalary,
                    description = item.Description,
                    Date = ((DateTime)item.date).ToString("MMM dd, yyyy"),
                    City = cityData.Name
                };
                model.Add(data);
            }
            return PartialView("_partialJobs", model);
        }

        [HttpPost]
        public ActionResult SaveContectData(string Name, string Email, string Mobile, string Description = "")
        {
            tblContectU data = new tblContectU();
            data.Name = Name;
            data.Email = Email;
            data.Mobile = Mobile;
            data.Description = Description;
            _db.tblContectUs.Add(data);
            _db.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}