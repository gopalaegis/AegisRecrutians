using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class ReferController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        public ActionResult Index(int jobId)
        {
            ReferViewModel model = new ReferViewModel();
            model.jobId = jobId;
            return View(model);
        }

        public ActionResult SaveData(ReferViewModel model)
        {
            tblRefer m = new tblRefer();
            m.FullName = model.Name;
            m.Email = model.Email;
            m.PhoneNumber = model.PhoneNo;
            m.FriendFullName = model.FriendName;
            m.FriendEmail = model.FriendEmail;
            m.FriendPhoneNo = model.FriendPhoneNo;
            m.Description = model.Description;
            m.JobId = model.jobId;
            _db.tblRefers.Add(m);
            _db.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}