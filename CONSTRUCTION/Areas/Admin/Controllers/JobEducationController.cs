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
    public class JobEducationController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Admin/JobEducation
        public ActionResult JobEducation()
        {
            return View();
        }
        public ActionResult AddEditJobEducationMaster(int Id =0)
        {
            JobEducationViewModel model = new JobEducationViewModel();
            if (Id > 0)
            {
                model = _db.tblJobEducations.Where(x => x.Id == Id).Select(x => new JobEducationViewModel { Id = x.Id, Education = x.Education }).FirstOrDefault();
            }
            return PartialView("_partialAddEducationMaster", model);
        }
        public ActionResult JobEducationMasterList()
        {
            List<JobEducationViewModel> model = new List<JobEducationViewModel>();
            model = _db.tblJobEducations.Select(x => new JobEducationViewModel { Id = x.Id, Education = x.Education,isactive=(bool)x.isActive }).ToList();
            return PartialView("_partialJobEducationMasterList", model);
        }
        //public ActionResult DeleteJobEducationMaster(int Id)
        //{
        //    var data = _db.tblJobEducations.Where(x => x.Id == Id).FirstOrDefault();
        //    _db.tblJobEducations.Remove(data);
        //    _db.SaveChanges();
        //    return RedirectToAction("JobEducationMasterList");
        //}
        public ActionResult isDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.tblJobEducations.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.tblJobEducations.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = true;
                _db.SaveChanges();
            }
            return RedirectToAction("JobEducationMasterList");
        }
        [HttpPost]
        public ActionResult SaveJobEducationMaster(JobEducationViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.tblJobEducations.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Education = model.Education;
                _db.SaveChanges();
            }
            else
            {
                tblJobEducation data = new tblJobEducation();
                data.Education = model.Education;
                data.isActive = true;
                _db.tblJobEducations.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("JobEducationMasterList");
        }
    }
}