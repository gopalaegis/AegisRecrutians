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

        public ActionResult TechnoloGyWiseJob(string slugURL)
        {
            int technologyId = 0;
            SEOModel seo = new SEOModel();
            var technologyName = _db.tblTechnologyMasters.Where(x => x.Slug == slugURL).FirstOrDefault();
            if (technologyName != null)
            {
                technologyId = technologyName.Id;
                seo.SEOtitle = technologyName.SEOtitle;
                seo.Slug = technologyName.Slug;
                seo.MetaDescription = technologyName.MetaDescription;
                seo.IsCrawl = Convert.ToBoolean(technologyName.IsCrawl);
                seo.IsCrawlString = Convert.ToBoolean(technologyName.IsCrawl) == true ? "index, follow" : "noindex, nofollow";
                seo.CanonicalURL = technologyName.CanonicalURL;
                seo.SchemaTags = technologyName.SchemaTags;
                seo.FocusKeyphrase = technologyName.FocusKeyphrase;
                seo.jobDescription = technologyName.BriefDescription;
                seo.cityId = 0;
            }
            else {
                var technologyNameCity = _db.tblAddCityTechMasters.Where(x => x.Slug == slugURL).FirstOrDefault();
                if (technologyNameCity != null)
                {
                    technologyId = Convert.ToInt32(technologyNameCity.TechMasterId);
                    var technologyData = _db.tblTechnologyMasters.Where(x => x.Id == technologyId).FirstOrDefault();
                    seo.SEOtitle = technologyNameCity.SEOtitle;
                    seo.Slug = technologyNameCity.Slug;
                    seo.MetaDescription = technologyNameCity.MetaDescription;
                    seo.IsCrawl = Convert.ToBoolean(technologyNameCity.IsCrawl);
                    seo.IsCrawlString = Convert.ToBoolean(technologyNameCity.IsCrawl) == true ? "index, follow" : "noindex, nofollow";
                    seo.CanonicalURL = technologyNameCity.CanonicalURL;
                    seo.SchemaTags = technologyNameCity.SchemaTags;
                    seo.FocusKeyphrase = technologyNameCity.FocusKeyphrase;
                    seo.jobDescription = technologyNameCity.BriefDescription;
                    seo.cityId = Convert.ToInt32(technologyNameCity.CityId);
                }
            }
            Tuple<int, SEOModel> tuple = new Tuple<int, SEOModel>(technologyId, seo);
            return View(tuple);
        }

        public ActionResult Index(string l = "", string q = "")
        {
            JobDetailViewModel model = new JobDetailViewModel();
            model.City = l.Replace('-', ' '); ;
            model.Title = q.Replace('-',' ');
            return View(model);
        }

        public ActionResult List(int page = 1, string city = "", string title = "", int TechnologyId = 0,int technologyCityId = 0)
        {
            JobsViewModel model = new JobsViewModel();
            List<JobDetailViewModel> m = new List<JobDetailViewModel>();

            var technologyWiseList = _db.JobWiseTechnologies.Where(x => x.TechnologyId == TechnologyId).Select(x => x.JobId).ToList();
            var d = _db.tblJobDetails.ToList();
            if (TechnologyId > 0)
            {
                d = d.Where(x => technologyWiseList.Contains(x.Id)).ToList();
            }
            foreach (var item in d)
            {
                var cityData = _db.tblCities.Where(x => x.Id == item.CityId).FirstOrDefault();
                JobDetailViewModel data = new JobDetailViewModel();

                data.Id = item.Id;
                data.Title = item.Title;
                data.CityId = (int)item.CityId;
                data.minExpirience = (int)item.MinExperience;
                data.maxExpirience = (int)item.MaxExperience;
                data.Date = ((DateTime)item.date).ToString("MMM dd, yyyy");
                if (TechnologyId > 0)
                {
                    if (technologyCityId > 0)
                    {
                        data.City = item.CityId == technologyCityId ? cityData.Name : "Work From Home";
                    }
                    else {
                        data.City = cityData.Name;
                    }
                }
                else
                {
                    data.City = cityData.Name.ToLower().Contains(city.ToLower()) ? cityData.Name : "Work From Home";
                }
                m.Add(data);
            }
            m = m.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
            model.currentPage = page;
            model.totalRecord = m.Count;
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
                model.fromtojob = (skip + 1).ToString() + " - " + (skip + model.jobDetailViewModels.Count) + " of " + model.totalRecord + " Jobs";
            }
            else
            {
                model.fromtojob = "0 - 0 of 0 Jobs";
            }
            return PartialView("_partialJobList", model);
        }
    }
}