using CONSTRUCTION.Areas.Admin.Model;
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
        public ActionResult JobDetail()
        {
            return View();
        }

        public ActionResult AddEditJobDetail(int Id = 0)
        {
            JobDetailsViewModel model = new JobDetailsViewModel();
            var categoryList = _db.tblCategoryMasters.ToList();
            List<SelectListItem> lstselectListItem = new List<SelectListItem>();

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
            if (Id > 0)
            {
                var data = _db.tblJobDetails.Where(x => x.Id == Id).FirstOrDefault();

                var cate = _db.tblCategoryMasters.Where(x => x.Id == data.CategoryId).FirstOrDefault();              
                
               
                var subcate = _db.tblSubCategoryMasters.Where(x => x.Id == data.SubCategoryId).FirstOrDefault();
                var country = _db.tblCountries.Where(x => x.Id == data.CountryId).FirstOrDefault();
                var city = _db.tblCities.Where(x => x.Id == data.CityId).FirstOrDefault();
                var state = _db.tblStates.Where(x => x.Id == data.StateId).FirstOrDefault();
                var jobequcation = _db.tblJobEducations.Where(x => x.Education == data.JobEducation).FirstOrDefault();
                var jobQualification = _db.tblJobQualifications.Where(x => x.Qualification == data.JobQualification).FirstOrDefault();

                data.Title = model.Title;
                data.Title = model.Title;
                data.CompanyName = model.CompanyName;
                data.CompanyLogo = model.CompanyLogo;
                data.CategoryId = cate.Id;               
                data.SubCategoryId = subcate.Id;
                data.Profession = model.Profession;
                data.CountryId = country.Id;
                data.StateId = model.Id;
                data.CityId = city.Id;
                data.Location = model.Location;
                data.Description = model.Description;
                data.MinExperience = Convert.ToInt32(model.MinExperience);
                data.MaxExperience = Convert.ToInt32(model.MaxExperience);
                data.Type = model.Type;
                data.MinSalary = Convert.ToInt32(model.MinSalary);
                data.MaxSalary = Convert.ToInt32(model.MaxSalary);
                data.JobEducation = jobequcation.Education;
                data.JobQualification = jobQualification.Qualification;
                data.IsVerified = Convert.ToBoolean(model.IsVerified);
                data.ShowOnHome = model.ShowOnHome;

            }
            return PartialView("_partialAddEditJobDetail", model);
        }

        public ActionResult JobDetailsList()
        {
            List<JobDetailsViewModel> model = new List<JobDetailsViewModel>();
            var data = _db.tblJobDetails.ToList();
            foreach (var item in data)
            {
                JobDetailsViewModel m = new JobDetailsViewModel();
                var cate = _db.tblCategoryMasters.Where(x => x.Id == item.CategoryId).FirstOrDefault();
                var subcate = _db.tblSubCategoryMasters.Where(x => x.Id == item.SubCategoryId).FirstOrDefault();
                var country = _db.tblCountries.Where(x => x.Id == item.CountryId).FirstOrDefault();
                var city = _db.tblCities.Where(x => x.Id == item.CityId).FirstOrDefault();
                var state = _db.tblStates.Where(x => x.Id == item.StateId).FirstOrDefault();
                var jobequcation = _db.tblJobEducations.Where(x => x.Education == item.JobEducation).FirstOrDefault();
                var jobQualification = _db.tblJobQualifications.Where(x => x.Qualification == item.JobQualification).FirstOrDefault();

                m.Title = item.Title;
                m.CompanyName = item.CompanyName;
                m.CompanyLogo = item.CompanyLogo;
                m.CategoryName = cate.Name;
                m.SubCategoryName = subcate.SubCategory;
                m.Profession = item.Profession;
                m.CountryName = country.Name;
                m.StateName = state.Name;
                m.CityName = city.Name;
                m.Location = item.Location;
                m.Description = item.Description;
                m.MinExperience = Convert.ToInt32(item.MinExperience);
                m.MaxExperience = Convert.ToInt32(item.MaxExperience);
                m.Type = item.Type;
                m.MinSalary = Convert.ToInt32(item.MinSalary);
                m.MaxSalary = Convert.ToInt32(item.MaxSalary);
                m.JobEducation = jobequcation.Education;
                m.JobQualification = jobQualification.Qualification;
                m.IsVerified = Convert.ToBoolean(item.IsVerified);
                m.ShowOnHome = item.ShowOnHome;
            }
            return PartialView("_partialJobDetailsList", model);
        }

        public ActionResult DeleteTechnologyMaster(int Id)
        {
            var data = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
            _db.tblTechnologyMasters.Remove(data);
            _db.SaveChanges();
            return RedirectToAction("TechnologyMasterList");
        }

        [HttpPost]
        public ActionResult SaveTechnologyMaster(TechnologyMasterViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.tblTechnologyMasters.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Image = model.Image;
                data.Name = model.Name;
                _db.SaveChanges();
            }
            else
            {
                tblTechnologyMaster data = new tblTechnologyMaster();
                data.Name = model.Name;
                data.Image = model.Image;
                _db.tblTechnologyMasters.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("TechnologyMasterList");
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
                uploadFile.SaveAs(Server.MapPath("~/CommonImage/Technology/") + fileName);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
    }
}