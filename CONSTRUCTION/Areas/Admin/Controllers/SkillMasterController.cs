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
    public class SkillMasterController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Admin/SkillMaster
        [UserLoginCheck]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditSkillMaster(int Id = 0)
        {
            SkillMasterViewModel model = new SkillMasterViewModel();
            if (Id > 0)
            {
                model = _db.tblSkillMasters.Where(x => x.Id == Id).Select(x => new SkillMasterViewModel { Id = x.Id, Name = x.Name }).FirstOrDefault();
            }
            return PartialView("_partialAddSkillMaster", model);
        }

        public ActionResult SkillMasterList(string Status = "active")
        {
            List<SkillMasterViewModel> model = new List<SkillMasterViewModel>();
            model = _db.tblSkillMasters.Select(x => new SkillMasterViewModel { Id = x.Id, Name = x.Name, isactive = (bool)x.isActive }).ToList();
            if (Status == "active")
            {
                model = model.Where(x => x.isactive == true).ToList();
            }
            else
            {
                model = model.Where(x => x.isactive == false).ToList();
            }
            return PartialView("_partialSkillMasterList", model);
        }

        //public ActionResult DeleteSkillMaster(int Id)
        //{
        //    var data = _db.tblSkillMasters.Where(x => x.Id == Id).FirstOrDefault();
        //    _db.tblSkillMasters.Remove(data);
        //    _db.SaveChanges();
        //    return RedirectToAction("SkillMasterList");
        //}

        [HttpPost]
        public ActionResult SaveSkillMaster(SkillMasterViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.tblSkillMasters.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Name = model.Name;
                _db.SaveChanges();
            }
            else
            {
                tblSkillMaster data = new tblSkillMaster();
                data.Name = model.Name;
                data.isActive = true;
                _db.tblSkillMasters.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("SkillMasterList");
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
                uploadFile.SaveAs(Server.MapPath("~/CommonImage/Skill/") + fileName);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult isDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.tblSkillMasters.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.tblSkillMasters.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = true;
                _db.SaveChanges();
            }

            string status = value == "deactive" ? "active" : "deactive";
            return RedirectToAction("SkillMasterList", new { Status = status });
        }
    }
}