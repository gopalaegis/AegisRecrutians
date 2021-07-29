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
            List<JobDetailViewModel> m = _db.tblJobDetails.Select(x => new JobDetailViewModel { Id = x.Id, Title = x.Title, CityId = (int)x.CityId, minExpirience = (int)x.MinExperience, maxExpirience = (int)x.MaxExperience, minSalary = (int)x.MinSalary, maxSalary = (int)x.MaxSalary, description = x.Description }).OrderByDescending(x => x.Id).ToList();
            foreach (var item in m)
            {
                var cityData = _db.tblCities.Where(x => x.Id == item.CityId).FirstOrDefault();
                if (cityData != null)
                {
                    item.City = cityData.Name;
                }
            }
            return PartialView("_partialJobs", m);
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