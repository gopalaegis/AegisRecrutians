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
    public class ReferController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        public ActionResult Index(int jobId)
        {
            var job = _db.tblJobDetails.Where(x => x.Id == jobId).FirstOrDefault();
            ReferViewModel model = new ReferViewModel();
            model.jobId = jobId;
            model.Job = job.Title;
            model.URL = Request.UrlReferrer.AbsoluteUri;
            return View(model);
        }

        public ActionResult SaveData(ReferViewModel model)
        {
            try
            {
            tblRefer m = new tblRefer();
            //m.FullName = model.Name;
            //m.Email = model.Email;
            //m.PhoneNumber = model.PhoneNo;
            //m.FriendFullName = model.FriendName;
            //m.FriendEmail = model.FriendEmail;
            //m.FriendPhoneNo = model.FriendPhoneNo;
            //m.Description = model.Description;
            m.JobId = model.jobId;
            //_db.tblRefers.Add(m);
            //_db.SaveChanges();

            string from = WebConfigurationManager.AppSettings["SMTPMailFrom"];
            string user = WebConfigurationManager.AppSettings["SMTPUserName"];
            string pass = WebConfigurationManager.AppSettings["SMTPPassword"];
            string to = WebConfigurationManager.AppSettings["SMTPMailTo"];
            int port = Convert.ToInt32(WebConfigurationManager.AppSettings["SMTPPort"]);
            bool ssl = Convert.ToBoolean(WebConfigurationManager.AppSettings["SMTPEnableSSL"]);
            string host = Convert.ToString(WebConfigurationManager.AppSettings["SMTPHost"]);
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string urlforApply  = string.Format("{0}ApplyJob?jobId={1}", baseUrl, m.JobId);
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(user, pass);
            client.Port = port;
            client.Host = host;
            client.EnableSsl = ssl;
            string str = string.Empty;
            str = "Hi " + model.FriendName;
            str = str + "<br />You are refered for below job by your friend " + model.Name + "<br />";
            str = str + "<a href ='" + urlforApply + "' title='Apply Job'> Click here to Apply </a><br />";
            str = str + "Thanks & Regards <br /> Aegis Team";
            MailMessage message = new MailMessage(from, model.FriendEmail, " Aegis Portal - Refer friend for job ", str);
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