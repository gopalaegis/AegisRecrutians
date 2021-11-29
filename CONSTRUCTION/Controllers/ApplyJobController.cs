using CONSTRUCTION.CommonMethods;
using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
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

            if (Request.UrlReferrer != null)
                if (!string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri))
                    model.URL = Request.UrlReferrer.AbsoluteUri;
                else
                    model.URL = "";
            else
                model.URL = "";

            model.JobId = jobId;
            if (job != null)
            {
                model.Job = job.Title;
            }
            model.drpState.Add(new SelectListItem { Value = "0", Text = "State" });
            model.drpCountry = _commonMethod.GetCountry();
            var country = model.drpCountry.Where(x => x.Value != "0").FirstOrDefault();
            if (country != null)
            {
                if (Convert.ToInt32(country.Value) > 0)
                {
                    model.drpState = _commonMethod.GetStateByCountry(Convert.ToInt32(country.Value));
                }
            }


            var schematag = _db.Schematag_master.Where(x => x.SchemaTag == "applyJob").FirstOrDefault();
            model.SchemaDescription = schematag != null ? schematag.description : "";
            if (!string.IsNullOrEmpty(model.SchemaDescription))
            {
                model.SchemaDescription = model.SchemaDescription.Replace("__JonName__", job.Title);
                model.SchemaDescription = model.SchemaDescription.Replace("__JobId__", job.Id.ToString());
            }
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
            model.drpState.Add(new SelectListItem { Value = "0", Text = "State" });
            model.drpCountry = _commonMethod.GetCountry();
            var country = model.drpCountry.Where(x => x.Value != "0").FirstOrDefault();
            if (country != null)
            {
                if (Convert.ToInt32(country.Value) > 0)
                {
                    model.drpState = _commonMethod.GetStateByCountry(Convert.ToInt32(country.Value));
                }
            }
            return PartialView("_partialExperience", model);
        }

        [HttpGet]
        public ActionResult GetStateByCountry(int Country)
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data = _commonMethod.GetStateByCountry(Country);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
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

            var jobData = _db.tblJobDetails.Where(x => x.Id == model.JobId).FirstOrDefault();

            tblApplyJobDetail data = new tblApplyJobDetail();
            data.Name = model.Name;
            data.DOB = Convert.ToDateTime(model.DOB);
            data.Gender = model.Gender;
            data.Email = model.Email;
            data.Mobile = model.PhoneNo1;
            //data.Mobile1 = model.PhoneNo2;
            //data.AddressType = model.AddressType;
            data.HouseName = model.BlockNo;
            data.Street = model.Street;
            data.City = model.City;
            data.CountryId = model.CountryId;
            data.StateId = model.StateId;
            data.JobId = model.JobId;
            data.Resume = model.Resume;
            data.isActive = true;
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
            try
            {
                string from = WebConfigurationManager.AppSettings["SMTPMailFrom"];
                string user = WebConfigurationManager.AppSettings["SMTPUserName"];
                string pass = WebConfigurationManager.AppSettings["SMTPPassword"];
                string to = WebConfigurationManager.AppSettings["SMTPMailTo"];
                int port = Convert.ToInt32(WebConfigurationManager.AppSettings["SMTPPort"]);
                bool ssl = Convert.ToBoolean(WebConfigurationManager.AppSettings["SMTPEnableSSL"]);
                string host = Convert.ToString(WebConfigurationManager.AppSettings["SMTPHost"]);
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                //string urlforApply = string.Format("{0}ApplyJob?jobId={1}", baseUrl, m.JobId);
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(user, pass);
                client.Port = port;
                client.Host = host;
                client.EnableSsl = ssl;
                string str = string.Empty;
                str = "Hi ";
                str = str + "Job Title : " + jobData.Title + "<br />";
                str = str + "Name : " + model.Name + "<br />";
                str = str + "Contact Email : " + model.Email + "<br />";
                str = str + "Contact Mobile : " + model.PhoneNo1 + "<br />";
                str = str + "Thanks & Regards <br /> Aegis Team";
                if (!string.IsNullOrEmpty(model.Resume))
                {
                    str = str + " <br/><br/><a href = '" + baseUrl + "CommonImage/Resume/" + model.Resume + "' style='background-color: #0aafce;color: #fff;padding: 12px;text-decoration: none;font-weight: bold;font-family: 'Roboto', sans-serif;' download target='_blank'> Download Resume</a>";
                }

                MailMessage message = new MailMessage(from, to, " Aegis Portal - Apply Job ", str);
                message.IsBodyHtml = true;
                //message.Headers.Add("Content-Type", "text/html");
                client.Send(message);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
    }
}