using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Controllers
{
    public class ReferDetailsController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReferList()
        {
            List<ReferViewModel> model = new List<ReferViewModel>();
            model = _db.tblRefers.Select(x => new ReferViewModel { Id = x.Id, Name = x.FullName, Email = x.Email, PhoneNo = x.PhoneNumber, FriendName = x.FriendFullName, FriendEmail = x.FriendEmail, FriendPhoneNo = x.FriendPhoneNo, Description = x.Description, Job = _db.tblJobDetails.Where(y => y.Id == x.JobId).FirstOrDefault().Title }).ToList();
            return PartialView("_partialReferList", model);
        }

    }
}