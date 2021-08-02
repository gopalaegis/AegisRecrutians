using CONSTRUCTION.CommonMethods;
using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class ApplyJobController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        CommonMethod _commonMethod = new CommonMethod();
        // GET: ApplyJob
        public ActionResult Index(int jobId)
        {
            var job = _db.tblJobDetails.Where(x => x.Id == jobId).FirstOrDefault();
            ApplyJobViewModel model = new ApplyJobViewModel();
            model.URL = Request.UrlReferrer.AbsoluteUri;
            model.JobId = jobId;
            if (job != null)
            {
                model.Job = job.Title;
            }
            model.drpCountry = _commonMethod.GetCountry();
            return View(model);
        }

        public ActionResult EducationData()
        {
            ApplyJobViewModel model = new ApplyJobViewModel();
            model.drpDegree = _commonMethod.GetJobEducation();
            return PartialView("_partialEducation", model);
        }

        public ActionResult ExperienceData()
        {
            ApplyJobViewModel model = new ApplyJobViewModel();
            model.drpCountry = _commonMethod.GetCountry();
            return PartialView("_partialExperience", model);
        }

        [HttpGet]
        public ActionResult GetStateByCountry(int Country)
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data = _commonMethod.GetStateByCountry(Country);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FileUplaod()
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            System.Web.HttpRequest req = System.Web.HttpContext.Current.Request;
            string fileName = "";
            string fileNamewithExtenstion = "";
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile uploadFile = hfc[i];
                fileName = Path.GetFileName(uploadFile.FileName);

                if (fileName.Trim().Length > 0)
                {
                    fileName = Guid.NewGuid() + fileName;
                    uploadFile.SaveAs(Server.MapPath("~/CommonImage/Resume/") + fileName);
                }
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveData(ApplyJobViewModel model)
        {
            var Education = new List<EducationViewModel>();
            var Experience = new List<ExperienceViewModel>();
            if (model.jsonEducation != null && model.jsonEducation != "")
            {
                Education = JsonConvert.DeserializeObject<List<EducationViewModel>>(model.jsonEducation);
            }
            if (model.jsonExperience != null && model.jsonExperience != "")
            {
                Experience = JsonConvert.DeserializeObject<List<ExperienceViewModel>>(model.jsonExperience);
            }

            tblApplyJobDetail data = new tblApplyJobDetail();
            data.Name= model.Name;
            data.DOB= Convert.ToDateTime(model.DOB);
            data.Gender= model.Gender;
            data.Email= model.Email;
            data.Mobile= model.PhoneNo1;
            data.Mobile1= model.PhoneNo2;
            data.AddressType= model.AddressType;
            data.HouseName= model.BlockNo;
            data.Street= model.Street;
            data.City= model.City;
            data.CountryId= model.CountryId;
            data.StateId= model.StateId;
            data.JobId= model.JobId;
            data.Resume= model.Resume;
            _db.tblApplyJobDetails.Add(data);
            _db.SaveChanges();

            if (Education.Count > 0)
            {
                foreach (var item in Education)
                {
                    tblApplyJobEducation m = new tblApplyJobEducation();
                    m.ApplyJobDetailId = data.Id;
                    m.DegreeId = item.DegreeId;
                    m.Collage = item.Collage;
                    m.PassingYear = item.PassingYear;
                    _db.tblApplyJobEducations.Add(m);
                    _db.SaveChanges();
                }
            }

            if (Experience.Count > 0)
            {
                foreach (var item in Experience)
                {
                    tblApplyJobExperience m = new tblApplyJobExperience();
                    m.ApplyJobDetailId = data.Id;
                    m.Employer = item.Employer;
                    m.JobTitle = item.JobTitle;
                    m.ActiveYear = item.ActiveYear;
                    m.City = item.City;
                    m.StateId = item.State;
                    m.CountryId = item.Country;
                    _db.tblApplyJobExperiences.Add(m);
                    _db.SaveChanges();
                }
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}