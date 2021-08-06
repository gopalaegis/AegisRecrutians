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
    public class JobqualificationController : Controller
    {
        // GET: Admin/Jobqualification
        AEGIS_Entities _db = new AEGIS_Entities();
        public ActionResult JobQualification()
        {
            return View();
        }
        public ActionResult AddEditJobQualification(int Id = 0)
        {
            JobQualicationViewModel model = new JobQualicationViewModel();
            if (Id > 0)
            {
                model = _db.tblJobQualifications.Where(x => x.Id == Id).Select(x => new JobQualicationViewModel{ Id = x.Id, Qualification= x.Qualification}).FirstOrDefault();
            }
            return PartialView("_partialAddEditQualificationMaster", model);
        }
        public ActionResult JobQualificationMasterList()
        {
            List<JobQualicationViewModel> model = new List<JobQualicationViewModel>();
            model = _db.tblJobQualifications.Select(x => new JobQualicationViewModel { Id = x.Id, Qualification= x.Qualification,isactive=(bool)x.isActive}).ToList();
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
            return RedirectToAction("JobQualificationMasterList");
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
                _db.tblJobQualifications.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("JobQualificationMasterList");
        }
    }
}