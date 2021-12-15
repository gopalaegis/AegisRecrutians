using CONSTRUCTION.Areas.Admin.Model;
using CONSTRUCTION.CommonMethods;
using CONSTRUCTION.DataTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Controllers
{
    public class JobDetailsController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        [UserLoginCheck]
        public ActionResult JobDetail()
        {
            return View();
        }

        public ActionResult AddEditJobDetail(int Id = 0)
        {
            JobDetailsViewModel model = new JobDetailsViewModel();
            var categoryList = _db.tblCategoryMasters.ToList();
            var subCategoryList = _db.tblSubCategoryMasters.ToList();
            var countryList = _db.tblCountries.ToList();
            var cityList = _db.tblCities.ToList();
            var professionList = _db.tblProfessions.ToList();
            var technologyList = _db.tblTechnologyMasters.Where(x => x.isActive == true).ToList();

            List<SelectListItem> lstselectListItem = new List<SelectListItem>();
            List<SelectListItem> subCategoryListItem = new List<SelectListItem>();
            List<SelectListItem> countryListItem = new List<SelectListItem>();
            List<SelectListItem> cityListItem = new List<SelectListItem>();
            List<SelectListItem> professionListItem = new List<SelectListItem>();
            List<SelectListItem> TechnologyItem = new List<SelectListItem>();

            //For Category List Item
            foreach (var category in categoryList)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                };
                lstselectListItem.Add(selectListItem);
            }
            model.CategoryList = lstselectListItem;

            //For SubCategory List Item
            foreach (var subCategory in subCategoryList)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = subCategory.SubCategory,
                    Value = subCategory.Id.ToString()
                };
                subCategoryListItem.Add(selectListItem);
            }
            model.SubCategoryList = subCategoryListItem;

            //For Country List Item
            foreach (var country in countryList)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id.ToString()
                };
                countryListItem.Add(selectListItem);
            }
            model.CountryList = countryListItem;

            //For CityListItem
            foreach (var city in cityList)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = city.Name,
                    Value = city.Id.ToString()
                };
                cityListItem.Add(selectListItem);
            }
            model.CityList = cityListItem;

            //For Profession
            foreach (var profession in professionList)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = profession.Profession,
                    Value = profession.Id.ToString()
                };
                professionListItem.Add(selectListItem);
            }
            model.ProfessionList = professionListItem;

            foreach (var tech in technologyList)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = tech.Name,
                    Value = tech.Id.ToString()
                };
                TechnologyItem.Add(selectListItem);
            }
            model.technologyList = TechnologyItem;


            if (Id > 0)
            {
                var data = _db.tblJobDetails.Where(x => x.Id == Id).FirstOrDefault();

                model.Id = data.Id;
                model.Title = data.Title;
                model.CompanyName = data.CompanyName;
                model.CompanyLogo = data.CompanyLogo;
                model.CategoryId = Convert.ToInt32(data.CategoryId);
                model.SubCategoryId = Convert.ToInt32(data.SubCategoryId);
                model.Profession = data.Profession;
                model.CountryId = Convert.ToInt32(data.CountryId);
                model.CityId = Convert.ToInt32(data.CityId);
                model.Location = data.Location;
                model.Description = data.Description;
                model.MinExperience = Convert.ToInt32(data.MinExperience);
                model.MaxExperience = Convert.ToInt32(data.MaxExperience);
                model.Type = data.Type;
                model.MinSalary = Convert.ToInt32(data.MinSalary);
                model.MaxSalary = Convert.ToInt32(data.MaxSalary);
                model.JobEducation = data.JobEducation;
                model.JobQualification = data.JobQualification;
                model.IsVerified = Convert.ToBoolean(data.IsVerified);
                model.ShowOnHome = data.ShowOnHome;
                model.BriefDescription = data.BriefDescription;
                model.KeyWord = data.KeyWords;
                model.SelectedTechnology = _db.JobWiseTechnologies.Where(x => x.JobId == Id).Select(x => (int)x.TechnologyId).ToList();
                model.SelectedCity = _db.JobWiseCities.Where(x => x.JobId == Id).Select(x => (int)x.CityId).ToList();
            }
            else
            {
                model.IsVerified = true;
                model.SelectedTechnology = new List<int>();
                model.SelectedCity = new List<int>();
            }
            return PartialView("_partialAddEditJobDetail", model);
        }

        public ActionResult JobDetailsList(string Status = "active")
        {
            List<JobDetailsViewModel> model = new List<JobDetailsViewModel>();
            var data = _db.tblJobDetails.ToList();

            var cateList = _db.tblCategoryMasters.ToList();
            var subcateList = _db.tblSubCategoryMasters.ToList();
            var countryList = _db.tblCountries.ToList();
            var cityList = _db.tblCities.ToList();
            var jobequcationList = _db.tblJobEducations.ToList();
            var jobQualificationList = _db.tblJobQualifications.ToList();
            var jobwiseCity = _db.JobWiseCities.ToList();
            foreach (var item in data)
            {
                JobDetailsViewModel m = new JobDetailsViewModel();
                var cate = cateList.Where(x => x.Id == item.CategoryId).FirstOrDefault();
                var subcate = subcateList.Where(x => x.Id == item.SubCategoryId).FirstOrDefault();
                var country = countryList.Where(x => x.Id == item.CountryId).FirstOrDefault();

                var jobequcation = jobequcationList.Where(x => x.Education == item.JobEducation).FirstOrDefault();
                var jobQualification = jobQualificationList.Where(x => x.Qualification == item.JobQualification).FirstOrDefault();
                var jobCity = jobwiseCity.Where(x => x.JobId == item.Id).Select(x => x.CityId).ToList();
                var city = cityList.Where(x => jobCity.Contains(x.Id)).Select(x => x.Name).ToList();
                m.Title = item.Title;
                m.Id = item.Id;
                m.CompanyName = item.CompanyName;
                m.CompanyLogo = item.CompanyLogo;
                if (cate != null)
                {
                    m.CategoryName = cate.Name;
                }
                if (subcate != null)
                {
                    m.SubCategoryName = subcate.SubCategory;
                }
                if (country != null)
                {
                    m.CountryName = country.Name;
                }
                if (city.Count > 0)
                {
                    m.CityName = string.Join(", ", city);
                }
                m.Profession = item.Profession;
                m.Location = item.Location;
                m.Description = item.Description;
                m.MinExperience = Convert.ToInt32(item.MinExperience);
                m.MaxExperience = Convert.ToInt32(item.MaxExperience);
                m.Type = item.Type;
                m.MinSalary = Convert.ToInt32(item.MinSalary);
                m.MaxSalary = Convert.ToInt32(item.MaxSalary);
                if (jobequcation != null)
                {
                    m.JobEducation = jobequcation.Education;
                }
                if (jobQualification != null)
                {
                    m.JobQualification = jobQualification.Qualification;
                }
                m.IsVerified = Convert.ToBoolean(item.IsVerified);
                m.ShowOnHome = item.ShowOnHome;
                m.isactive = (bool)item.isActive;
                model.Add(m);
            }
            if (Status == "active")
            {
                model = model.Where(x => x.isactive == true).ToList();
            }
            else
            {
                model = model.Where(x => x.isactive == false).ToList();
            }
            return PartialView("_partialJobDetailsList", model);
        }

        //public ActionResult DeletejobDeatilMaster(int Id)
        //{
        //    var data = _db.tblJobDetails.Where(x => x.Id == Id).FirstOrDefault();
        //    _db.tblJobDetails.Remove(data);
        //    _db.SaveChanges();
        //    return RedirectToAction("JobDetailsList");
        //}
        public ActionResult isDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.tblJobDetails.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.tblJobDetails.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = true;
                _db.SaveChanges();
            }
            string status = value == "deactive" ? "active" : "deactive";
            return RedirectToAction("JobDetailsList", new { Status = status });
        }

        [HttpPost]
        public ActionResult SaveJobDetailMaster(JobDetailsViewModel model)
        {

            if (model.Id > 0)
            {
                //var exist = _db.tblJobDetails.Where(x => x.Id != model.Id && x.Title.ToLower() == model.Title.ToLower()).ToList();
                //if (exist.Count > 0)
                //{
                //    return Json("Error", JsonRequestBehavior.AllowGet);
                //}
                var data = _db.tblJobDetails.Where(x => x.Id == model.Id).FirstOrDefault();

                data.Title = model.Title;
                data.CompanyName = model.CompanyName;
                data.CompanyLogo = model.CompanyLogo;
                data.CategoryId = model.CategoryId;
                data.SubCategoryId = model.SubCategoryId;
                data.Profession = model.Profession;
                data.CountryId = model.CountryId;
                data.CityId = model.CityId;
                data.Location = model.Location;
                data.Description = model.Description;
                data.MinExperience = Convert.ToInt32(model.MinExperience);
                data.MaxExperience = Convert.ToInt32(model.MaxExperience);
                data.Type = model.Type;
                data.MinSalary = Convert.ToInt32(model.MinSalary);
                data.MaxSalary = Convert.ToInt32(model.MaxSalary);
                data.JobEducation = model.JobEducation;
                data.JobQualification = model.JobQualification;
                data.IsVerified = Convert.ToBoolean(model.IsVerified);
                data.ShowOnHome = model.ShowOnHome;
                data.BriefDescription = model.BriefDescription;
                data.KeyWords = model.KeyWord;
                data.date = DateTime.Now;
                _db.SaveChanges();

                var removeoldData = _db.JobWiseTechnologies.Where(x => x.JobId == data.Id).ToList();
                _db.JobWiseTechnologies.RemoveRange(removeoldData);
                _db.SaveChanges();

                if (!string.IsNullOrEmpty(model.SelectedTechnologyString))
                {
                    if (model.SelectedTechnologyString.IndexOf(',') > -1)
                    {
                        foreach (var item in model.SelectedTechnologyString.Split(','))
                        {
                            JobWiseTechnology ip = new JobWiseTechnology();
                            ip.TechnologyId = Convert.ToInt32(item);
                            ip.JobId = data.Id;
                            _db.JobWiseTechnologies.Add(ip);
                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        JobWiseTechnology ip = new JobWiseTechnology();
                        ip.TechnologyId = Convert.ToInt32(model.SelectedTechnologyString);
                        ip.JobId = data.Id;
                        _db.JobWiseTechnologies.Add(ip);
                        _db.SaveChanges();
                    }
                }

                var removeoldCityData = _db.JobWiseCities.Where(x => x.JobId == data.Id).ToList();
                _db.JobWiseCities.RemoveRange(removeoldCityData);
                _db.SaveChanges();

                if (!string.IsNullOrEmpty(model.SelectedCityString))
                {
                    if (model.SelectedCityString.IndexOf(',') > -1)
                    {
                        foreach (var item in model.SelectedCityString.Split(','))
                        {
                            JobWiseCity ip = new JobWiseCity();
                            ip.CityId = Convert.ToInt32(item);
                            ip.JobId = data.Id;
                            _db.JobWiseCities.Add(ip);
                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        JobWiseCity ip = new JobWiseCity();
                        ip.CityId = Convert.ToInt32(model.SelectedCityString);
                        ip.JobId = data.Id;
                        _db.JobWiseCities.Add(ip);
                        _db.SaveChanges();
                    }
                }
            }
            else
            {
                //var exist = _db.tblJobDetails.Where(x => x.Title.ToLower() == model.Title.ToLower()).ToList();
                //if (exist.Count > 0)
                //{
                //    return Json("Error", JsonRequestBehavior.AllowGet);
                //}
                tblJobDetail data = new tblJobDetail();
                data.Title = model.Title;
                data.Title = model.Title;
                data.CompanyName = model.CompanyName;
                data.CompanyLogo = model.CompanyLogo;
                data.CategoryId = model.CategoryId;
                data.SubCategoryId = model.SubCategoryId;
                data.Profession = model.Profession;
                data.CountryId = model.CountryId;
                data.CityId = model.CityId;
                data.Location = model.Location;
                data.Description = model.Description;
                data.MinExperience = Convert.ToInt32(model.MinExperience);
                data.MaxExperience = Convert.ToInt32(model.MaxExperience);
                data.Type = model.Type;
                data.MinSalary = Convert.ToInt32(model.MinSalary);
                data.MaxSalary = Convert.ToInt32(model.MaxSalary);
                data.JobEducation = model.JobEducation;
                data.JobQualification = model.JobQualification;
                data.IsVerified = Convert.ToBoolean(model.IsVerified);
                data.ShowOnHome = model.ShowOnHome;
                data.BriefDescription = model.BriefDescription;
                data.KeyWords = model.KeyWord;
                data.date = DateTime.Now;
                data.isActive = true;
                _db.tblJobDetails.Add(data);
                _db.SaveChanges();

                if (!string.IsNullOrEmpty(model.SelectedTechnologyString))
                {
                    if (model.SelectedTechnologyString.IndexOf(',') > -1)
                    {
                        foreach (var item in model.SelectedTechnologyString.Split(','))
                        {
                            JobWiseTechnology ip = new JobWiseTechnology();
                            ip.TechnologyId = Convert.ToInt32(item);
                            ip.JobId = data.Id;
                            _db.JobWiseTechnologies.Add(ip);
                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        JobWiseTechnology ip = new JobWiseTechnology();
                        ip.TechnologyId = Convert.ToInt32(model.SelectedTechnologyString);
                        ip.JobId = data.Id;
                        _db.JobWiseTechnologies.Add(ip);
                        _db.SaveChanges();
                    }
                }

                if (!string.IsNullOrEmpty(model.SelectedCityString))
                {
                    if (model.SelectedCityString.IndexOf(',') > -1)
                    {
                        foreach (var item in model.SelectedCityString.Split(','))
                        {
                            JobWiseCity ip = new JobWiseCity();
                            ip.CityId = Convert.ToInt32(item);
                            ip.JobId = data.Id;
                            _db.JobWiseCities.Add(ip);
                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        JobWiseCity ip = new JobWiseCity();
                        ip.CityId = Convert.ToInt32(model.SelectedCityString);
                        ip.JobId = data.Id;
                        _db.JobWiseCities.Add(ip);
                        _db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("JobDetailsList");
        }

        public ActionResult FileUplaod()
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            System.Web.HttpRequest req = System.Web.HttpContext.Current.Request;
            string fileName = "";
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile uploadFile = hfc[i];
                fileName = Path.GetExtension(uploadFile.FileName);
                fileName = Guid.NewGuid() + fileName;
                uploadFile.SaveAs(Server.MapPath("~/CommonImage/CompanyLogo/") + fileName);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
    }
}