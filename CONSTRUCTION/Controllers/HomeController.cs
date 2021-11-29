using CONSTRUCTION.Areas.Admin.Model;
using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class HomeController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();


        public ActionResult autoCompleteDemo()
        {
            return View();
        }

        public ActionResult BindAutoComplete()
        {
            var cityData = _db.tblCities.Select(x => x.Name).Distinct().ToList();
            var jobTitle = _db.tblJobDetails.Select(x => x.Title).Distinct().ToList();
            Tuple<List<string>, List<string>> tuple = new Tuple<List<string>, List<string>>(cityData, jobTitle);
            return Json(tuple, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.skillMasterViewModels = _db.tblSkillMasters.Where(x => x.isActive == true).Select(x => new SkillMasterViewModel { Id = x.Id, Name = x.Name }).ToList();
            model.technologyMasterViewModels = _db.tblTechnologyMasters.Where(x => x.isActive == true).Select(x => new TechnologyMasterViewModel { Id = x.Id, Name = x.Name, Image = x.Image, Slug = x.Slug }).ToList();

            var schematag = _db.Schematag_master.Where(x => x.SchemaTag == "Home").FirstOrDefault();
            model.SchemaDescription = schematag != null ? schematag.description : "";
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

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult JobDetails()
        {
            List<JobDetailViewModel> model = new List<JobDetailViewModel>();
            var m = _db.tblJobDetails.Where(x => x.ShowOnHome == "1" && x.isActive == true).OrderByDescending(x => x.Id).ToList();
            var jobwiseCity = _db.JobWiseCities.ToList();
            var cityData = _db.tblCities.ToList();
            foreach (var item in m)
            {
                var jobCity = jobwiseCity.Where(x => x.JobId == item.Id).Select(x => x.CityId).ToList();
                var city = cityData.Where(x => jobCity.Contains(x.Id)).Select(x => x.Name).ToList();
                JobDetailViewModel data = new JobDetailViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    minExpirience = (int)item.MinExperience,
                    maxExpirience = (int)item.MaxExperience,
                    minSalary = (int)item.MinSalary,
                    maxSalary = (int)item.MaxSalary,
                    description = item.Description,
                    Date = ((DateTime)item.date).ToString("MMM dd, yyyy"),
                    City = city.Count > 0 ? string.Join(", ", city) : ""
                };
                model.Add(data);
            }
            return PartialView("_partialJobs", model);
        }

        [HttpPost]
        public ActionResult SaveContectData(string Email, string Mobile, string Description = "", string Skype = "", string Name = "")
        {
            try
            {
                string from = WebConfigurationManager.AppSettings["SMTPMailFrom"];
                string user = WebConfigurationManager.AppSettings["SMTPUserName"];
                string pass = WebConfigurationManager.AppSettings["SMTPPassword"];
                string to = WebConfigurationManager.AppSettings["SMTPMailTo"];
                int port = Convert.ToInt32(WebConfigurationManager.AppSettings["SMTPPort"]);
                bool ssl = Convert.ToBoolean(WebConfigurationManager.AppSettings["SMTPEnableSSL"]);
                string host = Convert.ToString(WebConfigurationManager.AppSettings["SMTPHost"]);

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(user, pass);
                client.Port = port;
                client.Host = host;
                client.EnableSsl = ssl;
                string str = string.Empty;
                str = "Hi <br /> <br /> ";
                if (Name != "")
                {
                    str = str + "Name :- " + Name + " <br />";
                }
                if (Skype != "")
                {
                    str = str + "Skype :- " + Skype + " <br />";
                }
                str = str + " Email : " + Email + "<br /> Mobile " + Mobile + "  <br />";
                str = str + "Description :- " + Description;
                MailMessage message = new MailMessage(from, to, " Your Review ", str);
                message.IsBodyHtml = true;
                //message.Headers.Add("Content-Type", "text/html");
                client.Send(message);

                tblContectU data = new tblContectU();
                data.Name = Name;
                data.Skype = Skype;
                data.Email = Email;
                data.Mobile = Mobile;
                data.Description = Description;
                _db.tblContectUs.Add(data);
                _db.SaveChanges();

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}