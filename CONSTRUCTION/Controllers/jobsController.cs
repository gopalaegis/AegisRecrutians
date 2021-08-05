using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class jobsController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        
        public ActionResult JobMain(int TechnologyId = 0)
        {
            var technologyName = _db.tblTechnologyMasters.Where(x => x.Id == TechnologyId).FirstOrDefault();
            string tec = "";
            if (technologyName != null)
                tec = technologyName.Name;
            Session["TechnologyId"] = TechnologyId;
            return RedirectToAction("Index", new { Technology = tec });
        }
 
        public ActionResult Index(string City = "", string Title = "", int TechnologyId = 0)
        {
            var data = City.Split('_');
            JobDetailViewModel model = new JobDetailViewModel();
            model.City = data.FirstOrDefault();
            model.Title = data.LastOrDefault();
            model.TechnologyId = Convert.ToInt32(TechnologyId);
            var technologyName = _db.tblTechnologyMasters.Where(x => x.Id == model.TechnologyId).FirstOrDefault();
            string tec = "";
            if (technologyName != null)
                tec = technologyName.Name;
            model.TechnologyTitle = tec;
            return View(model);
        }

        public ActionResult List(int page = 1, string city = "", string title = "", int TechnologyId = 0)
        {
            JobsViewModel model = new JobsViewModel();
            List<JobDetailViewModel> m = new List<JobDetailViewModel>();

            var technologyWiseList = _db.JobWiseTechnologies.Where(x => x.TechnologyId == TechnologyId).Select(x => x.JobId).ToList();
            var d = _db.tblJobDetails.ToList();
            if (TechnologyId > 0) {
                d = d.Where(x => technologyWiseList.Contains(x.Id)).ToList();
            }
            foreach (var item in d)
            {
                var cityData = _db.tblCities.Where(x => x.Id == item.CityId).FirstOrDefault();
                JobDetailViewModel data = new JobDetailViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    CityId = (int)item.CityId,
                    minExpirience = (int)item.MinExperience,
                    maxExpirience = (int)item.MaxExperience,
                    Date = ((DateTime)item.date).ToString("MMM dd, yyyy"),
                    City = cityData.Name
                };
                m.Add(data);
            }
            m = m.Where(x => x.City.ToLower().Contains(city.ToLower()) && x.Title.ToLower().Contains(title.ToLower())).ToList();
            model.currentPage = page;
            var pageCount = (double)m.Count / (double)5;
            model.totalPage = (int)Math.Ceiling(pageCount);
            var skip = ((page - 1) * 5);
            m = m.Skip(skip).ToList();
            if (m.Count > 5)
            {
                model.jobDetailViewModels = m.Take(5).ToList();
            }
            else
            {
                model.jobDetailViewModels = m.ToList();
            }
            if (model.jobDetailViewModels.Count > 0)
            {
                model.fromtojob = (skip + 1).ToString() + " - " + (skip + model.jobDetailViewModels.Count) + " of " + model.totalPage + " Jobs";
            }
            else
            {
                model.fromtojob = "0 - 0 of 0 Jobs";
            }
            return View("_partialJobList", model);
        }
    }
}