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
    public class JobqualificationController : Controller
    {
        // GET: Admin/Jobqualification
        AEGIS_Entities _db = new AEGIS_Entities();
        [UserLoginCheck]
        public ActionResult JobQualification()
        {
            return View();
        }
        public ActionResult AddEditJobQualification(int Id = 0)
        {
            JobQualicationViewModel model = new JobQualicationViewModel();
            if (Id > 0)
            {
                model = _db.tblJobQualifications.Where(x => x.Id == Id).Select(x => new JobQualicationViewModel{ Id = x.Id, Qualification= x.Qualification, isactive = (bool)x.isActive }).FirstOrDefault();
            }
            return PartialView("_partialAddEditQualificationMaster", model);
        }
        public ActionResult JobQualificationMasterList(string Status = "active")
        {
            List<JobQualicationViewModel> model = new List<JobQualicationViewModel>();
            model = _db.tblJobQualifications.Select(x => new JobQualicationViewModel { Id = x.Id, Qualification= x.Qualification,isactive=(bool)x.isActive}).ToList();
            if (Status == "active")
            {
                model = model.Where(x => x.isactive == true).ToList();
            }
            else
            {
                model = model.Where(x => x.isactive == false).ToList();
            }
            return PartialView("_partialJobQualificationMasterList", model);
        }
        //public ActionResult DeleteJobQualificationMaster(int Id)
        //{
        //    var data = _db.tblJobQualifications.Where(x => x.Id == Id).FirstOrDefault();
        //    _db.tblJobQualifications.Remove(data);
        //    _db.SaveChanges();
        //    return RedirectToAction("JobQualificationMasterList");
        //}
        public ActionResult isDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.tblJobQualifications.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.tblJobQualifications.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = true;
                _db.SaveChanges();
            }

            string status = value == "deactive" ? "active" : "deactive";
            return RedirectToAction("JobQualificationMasterList", new { Status = status });
        }

        [HttpPost]
        public ActionResult SaveJobQualificationMaster(JobQualicationViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.tblJobQualifications.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Qualification = model.Qualification;
                _db.SaveChanges();
            }
            else
            {
                tblJobQualification data = new tblJobQualification();
                data.Qualification = model.Qualification;
                data.isActive = true;
                _db.tblJobQualifications.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("JobQualificationMasterList");
        }
    }
}