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
    public class JobDetailsController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditTechnologyMaster(int Id = 0)
        {
            TechnologyMasterViewModel model = new TechnologyMasterViewModel();
            if (Id > 0)
            {
                model = _db.tblTechnologyMasters.Where(x => x.Id == Id).Select(x => new TechnologyMasterViewModel { Id = x.Id, Name = x.Name, Image = x.Image }).FirstOrDefault();
            }
            return PartialView("_partialAddTechnologyMaster", model);
        }

        public ActionResult JobDetailsList()
        {
            List<JobDetailsViewModel> model = new List<JobDetailsViewModel>();
            var data = _db.tblJobDetails.ToList();
            foreach (var item in data)
            {
                JobDetailsViewModel m = new JobDetailsViewModel();
                var cate = _db.tblCategoryMasters.Where(x => x.Id == item.CategoryId).FirstOrDefault();
                var subcate = _db.tblSubCategoryMasters.Where(x => x.Id == item.SubCategoryId).FirstOrDefault();
                var country = _db.tblCountries.Where(x => x.Id == item.CountryId).FirstOrDefault();
                var state = _db.tblStates.Where(x => x.Id == item.StateId).FirstOrDefault();
                m.Title = item.Title;
                m.CompanyName = item.CompanyName;
                m.CompanyLogo = item.CompanyLogo;
                m.CategoryName = cate.Name;
                m.SubCategoryName = subcate.SubCategory;
                m.Profession = item.Profession;
                m.CountryName = country.Name;
                m.StateName = item.Title;
                m.Title = item.Title;
                m.Title = item.Title;
                m.Title = item.Title;
                m.Title = item.Title;
                m.Title = item.Title;
                m.Title = item.Title;
            }
            return PartialView("_partialTechnologyMasterList", model);
        }

        public ActionResult DeleteTechnologyMaster(int Id)
        {
            var data = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
            _db.tblTechnologyMasters.Remove(data);
            _db.SaveChanges();
            return RedirectToAction("TechnologyMasterList");
        }

        [HttpPost]
        public ActionResult SaveTechnologyMaster(TechnologyMasterViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.tblTechnologyMasters.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Image = model.Image;
                data.Name = model.Name;
                _db.SaveChanges();
            }
            else
            {
                tblTechnologyMaster data = new tblTechnologyMaster();
                data.Name = model.Name;
                data.Image = model.Image;
                _db.tblTechnologyMasters.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("TechnologyMasterList");
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
                uploadFile.SaveAs(Server.MapPath("~/CommonImage/Technology/") + fileName);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
    }
}