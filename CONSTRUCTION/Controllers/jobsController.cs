using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            var technologyName = _db.tblTechnologyMasters.Where(x => x.Slug == slugURL && x.isActive == true).FirstOrDefault();
            if (technologyName != null)
            {
                technologyId = technologyName.Id;
                seo.SEOtitle = technologyName.SEOtitle;
                seo.H1 = technologyName.H1;
                seo.Slug = technologyName.Slug;
                seo.MetaDescription = technologyName.MetaDescription;
                seo.IsCrawl = Convert.ToBoolean(technologyName.IsCrawl);
                seo.IsCrawlString = Convert.ToBoolean(technologyName.IsCrawl) == true ? "index, follow" : "noindex, nofollow";
                seo.CanonicalURL = technologyName.CanonicalURL;
                seo.SchemaTags = technologyName.SchemaTags;
                seo.FocusKeyphrase = technologyName.FocusKeyphrase;
                seo.jobDescription = technologyName.BriefDescription;
                seo.cityId = 0;
                seo.Id = technologyName.Id;
            }
            else
            {
                var technologyNameCity = _db.tblAddCityTechMasters.Where(x => x.Slug == slugURL && x.isActive == true).FirstOrDefault();
                if (technologyNameCity != null)
                {
                    technologyId = Convert.ToInt32(technologyNameCity.TechMasterId);
                    var technologyData = _db.tblTechnologyMasters.Where(x => x.Id == technologyId).FirstOrDefault();
                    seo.SEOtitle = technologyNameCity.SEOtitle;
                    seo.H1 = technologyNameCity.H1;
                    seo.Slug = technologyNameCity.Slug;
                    seo.MetaDescription = technologyNameCity.MetaDescription;
                    seo.IsCrawl = Convert.ToBoolean(technologyNameCity.IsCrawl);
                    seo.IsCrawlString = Convert.ToBoolean(technologyNameCity.IsCrawl) == true ? "index, follow" : "noindex, nofollow";
                    seo.CanonicalURL = technologyNameCity.CanonicalURL;
                    seo.SchemaTags = technologyNameCity.SchemaTags;
                    seo.FocusKeyphrase = technologyNameCity.FocusKeyphrase;
                    seo.jobDescription = technologyNameCity.BriefDescription;
                    seo.cityId = Convert.ToInt32(technologyNameCity.CityId);
                    seo.Id = technologyNameCity.Id;
                }
            }
            Tuple<int, SEOModel> tuple = new Tuple<int, SEOModel>(technologyId, seo);
            return View(tuple);
        }

        public ActionResult Index(string l = "", string q = "")
        {
            JobDetailViewModel model = new JobDetailViewModel();
            model.City = l.Replace('-', ' '); ;
            model.Title = q.Replace('-', ' ');
            return View(model);
        }

        public ActionResult BindHTMLCity(int Id, int CityId)
        {
            string htmlString = "";
            if (CityId > 0)
            {
                var technologyNameCity = _db.tblAddCityTechMasters.Where(x => x.Id == Id).FirstOrDefault();
                if (!string.IsNullOrEmpty(technologyNameCity.BriefDescription))
                {
                    var c = new HtmlString(technologyNameCity.ToString());
                    htmlString = Regex.Replace(technologyNameCity.BriefDescription, "(?<=\\<[^<>]*)\"(?=[^><]*\\>)", "'");
                }
            }
            else
            {
                var technologyNameCity = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
                if (!string.IsNullOrEmpty(technologyNameCity.BriefDescription))
                {
                    var c = new HtmlString(technologyNameCity.ToString());
                    htmlString = Regex.Replace(technologyNameCity.BriefDescription, "(?<=\\<[^<>]*)\"(?=[^><]*\\>)", "'");
                }
            }
            SEOModel seo = new SEOModel();
            seo.jobDescription = htmlString;
            return View(seo);
        }

        public ActionResult List(int page = 1, string city = "", string title = "", int TechnologyId = 0, int technologyCityId = 0)
        {
            JobsViewModel model = new JobsViewModel();
            List<JobDetailViewModel> m = new List<JobDetailViewModel>();
            title = title.ToLower();
            var technologyWiseList = _db.JobWiseTechnologies.Where(x => x.TechnologyId == TechnologyId).Select(x => x.JobId).ToList();
            var d = _db.tblJobDetails.Where(x => x.Title.ToLower().Contains(title) || x.KeyWords.ToLower().Contains(title)).ToList();
            d = d.Where(x => x.isActive == true).ToList();
            if (TechnologyId > 0)
            {
                d = d.Where(x => technologyWiseList.Contains(x.Id)).ToList();
            }
            var jobwiseCity = _db.JobWiseCities.ToList();
            var cityList = _db.tblCities.ToList();
            foreach (var item in d)
            {
                var jobCity = jobwiseCity.Where(x => x.JobId == item.Id).Select(x => x.CityId).ToList();
                var cityData = cityList.Where(x => jobCity.Contains(x.Id)).Select(x => x.Name).ToList();

                JobDetailViewModel data = new JobDetailViewModel();

                data.Id = item.Id;
                data.Title = item.Title;
                data.KeyWord = item.KeyWords;
                data.CityId = (int)item.CityId;
                data.minExpirience = (int)item.MinExperience;
                data.maxExpirience = (int)item.MaxExperience;
                data.Date = ((DateTime)item.date).ToString("MMM dd, yyyy");
                if (TechnologyId > 0)
                {
                    if (technologyCityId > 0)
                    {
                        var currentCity = jobCity.Where(x => x == technologyCityId).FirstOrDefault();
                        if (currentCity != null)
                        {
                            data.City = cityList.Where(x => x.Id == technologyCityId).FirstOrDefault().Name;
                        }
                        else
                        {
                            data.City = "Work From Home";
                        }
                    }
                    else
                    {

                        data.City = string.Join(", ", cityData);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(city))
                    {
                        string cityName = "Work From Home";
                        foreach (var c in cityData)
                        {
                            if (c.ToLower().IndexOf(city.ToLower()) > -1)
                            {
                                cityName = c;
                            }
                        }
                        data.City = cityName;
                    }
                    else
                    {
                        if (cityData.Count > 0)
                        {
                            data.City = string.Join(", ", cityData);
                        }
                    }
                    //data.City = cityData.Name.ToLower().Contains(city.ToLower()) ? cityData.Name : "Work From Home";
                }
                m.Add(data);
            }
            m = m.Where(x => x.Title.ToLower().Contains(title.ToLower()) || x.KeyWord.ToLower().Contains(title)).ToList();
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