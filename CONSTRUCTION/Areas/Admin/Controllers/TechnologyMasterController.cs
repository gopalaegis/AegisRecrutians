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
    public class TechnologyMasterController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Admin/TechnologyMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditTechnologyMaster(int Id = 0)
        {
            TechnologyMasterViewModel model = new TechnologyMasterViewModel();
            if (Id > 0)
            {
                var data = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
                if (data!=null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Image = data.Image;
                    model.FocusKeyphrase = data.FocusKeyphrase;
                    model.SEOtitle = data.SEOtitle; 
                    model.Slug = data.Slug; 
                    model.MetaDescription = data.MetaDescription;
                    model.IsCrawl = (bool)data.IsCrawl;
                    model.CanonicalURL = data.CanonicalURL;
                    model.SchemaTags = data.SchemaTags;
                }
            }
            return PartialView("_partialAddTechnologyMaster", model);
        }

        public ActionResult AddEditCityMaster(int Id = 0)
        {
            TechnologyMasterViewModel model = new TechnologyMasterViewModel();
            if (Id > 0)
            {
                var data = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Image = data.Image;
                    model.FocusKeyphrase = data.FocusKeyphrase;
                    model.SEOtitle = data.SEOtitle;
                    model.Slug = data.Slug;
                    model.MetaDescription = data.MetaDescription;
                    model.IsCrawl = (bool)data.IsCrawl;
                    model.CanonicalURL = data.CanonicalURL;
                    model.SchemaTags = data.SchemaTags;
                }
            }
            return PartialView("_partialAddCityMaster", model);
        }
        public ActionResult TechnologyMasterList()
        {
            List<TechnologyMasterViewModel> model = new List<TechnologyMasterViewModel>();
            var data = _db.tblTechnologyMasters.ToList();
            foreach (var item in data)
            {
                TechnologyMasterViewModel m = new TechnologyMasterViewModel();
                m.Id = item.Id;
                m.Name = item.Name; 
                m.Image = item.Image;
                m.isactive = (bool)item.isActive;
                model.Add(m);
            }
            return PartialView("_partialTechnologyMasterList", model);
        }
        public ActionResult TechnologyCitylist()
        {
            List<TechnologyMasterViewModel> model = new List<TechnologyMasterViewModel>();
            var data = _db.tblTechnologyMasters.ToList();
            foreach (var item in data)
            {
                TechnologyMasterViewModel m = new TechnologyMasterViewModel();
                m.FocusKeyphrase = item.FocusKeyphrase;
                m.SEOtitle = item.SEOtitle;
                m.Slug = item.Slug;
                m.MetaDescription = item.MetaDescription;
                m.IsCrawl = (bool)item.IsCrawl;
                m.CanonicalURL = item.CanonicalURL;
                m.SchemaTags = item.SchemaTags;
                model.Add(m);
            }
            return PartialView("_partialTechnologyCityList", model);
        }
        //public ActionResult DeleteTechnologyMaster(int Id)
        //{
        //    var data = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
        //    _db.tblTechnologyMasters.Remove(data);
        //    _db.SaveChanges();
        //    return RedirectToAction("TechnologyMasterList");
        //}

        [HttpPost]
        public ActionResult SaveTechnologyMaster(TechnologyMasterViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.tblTechnologyMasters.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Image = model.Image;
                data.Name = model.Name;
                data.FocusKeyphrase = model.FocusKeyphrase;
                data.SEOtitle = model.SEOtitle;
                data.Slug = model.Slug;
                data.MetaDescription = model.MetaDescription;
                data.IsCrawl = model.IsCrawl;
                data.CanonicalURL = model.CanonicalURL;
                data.SchemaTags = model.SchemaTags;
                _db.SaveChanges();
            }
            else
            {
                tblTechnologyMaster data = new tblTechnologyMaster();
                data.Name = model.Name;
                data.Image = model.Image;
                data.FocusKeyphrase = model.FocusKeyphrase;
                data.SEOtitle = model.SEOtitle;
                data.Slug = model.Slug;
                data.MetaDescription = model.MetaDescription;
                data.IsCrawl = model.IsCrawl;
                data.CanonicalURL = model.CanonicalURL;
                data.SchemaTags = model.SchemaTags;
                data.isActive = true;
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
        public ActionResult isDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.tblTechnologyMasters.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = true;
                _db.SaveChanges();
            }
            return RedirectToAction("TechnologyMasterList");
        }
    }
}