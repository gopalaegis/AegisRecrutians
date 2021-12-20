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
    public class ContentMasterController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Admin/ContentMaster
        [UserLoginCheck]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditContentMaster(int Id = 0)
        {
            ContentMasterViewModel model = new ContentMasterViewModel();
            if (Id > 0)
            {
                var data = _db.tblContentMasters.Where(x => x.Id == Id).Select(x => new ContentMasterViewModel { Id = x.Id, Title = x.Title, Controller = x.Controller, ActionResult = x.ActionResult, FocusKeyphrase = x.FocusKeyphrase, SEOtitle = x.SEOtitle, Slug = x.Slug, MetaDescription = x.MetaDescription, IsCrawl = (bool)x.IsCrawl, CanonicalURL = x.CanonicalURL, SchemaTags = x.SchemaTags }).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Title = data.Title;
                    model.Controller = data.Controller;
                    model.ActionResult = data.ActionResult;
                    model.FocusKeyphrase = data.FocusKeyphrase;
                    model.SEOtitle = data.SEOtitle;
                    model.Slug = data.Slug;
                    model.MetaDescription = data.MetaDescription;
                    model.IsCrawl = (bool)data.IsCrawl;
                    model.CanonicalURL = data.CanonicalURL;
                    model.SchemaTags = data.SchemaTags;
                }
            }
            return PartialView("_partialAddContentMaster", model);
        }

        public ActionResult ContentMasterList(string Status = "active")
        {
            List<ContentMasterViewModel> model = new List<ContentMasterViewModel>();
            var data = _db.tblContentMasters.ToList();
            foreach (var item in data)
            {
                ContentMasterViewModel m = new ContentMasterViewModel();
                m.Id = item.Id;
                m.Title = item.Title;
                m.Controller = item.Controller;
                m.ActionResult = item.ActionResult;
                m.isactive = (bool)item.isActive;
                model.Add(m);
            }
            if (Status == "active")
            {
                model = model.Where(x => x.isactive == true).ToList();
            }
            else
            {
                model = model.Where(x => x.isactive == false).ToList();
            }
            return PartialView("_partialContentMasterList", model);
        }

        //public ActionResult DeleteContentMaster(int Id)
        //{
        //    var data = _db.tblContentMasters.Where(x => x.Id == Id).FirstOrDefault();
        //    _db.tblContentMasters.Remove(data);
        //    _db.SaveChanges();
        //    return RedirectToAction("ContentMasterList");
        //}

        [HttpPost]
        public ActionResult SaveContentMaster(ContentMasterViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.tblContentMasters.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Title = model.Title;
                data.Controller = model.Controller;
                data.ActionResult = model.ActionResult;
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
                tblContentMaster data = new tblContentMaster();
                data.Title = model.Title;
                data.Controller = model.Controller;
                data.ActionResult = model.ActionResult;
                data.FocusKeyphrase = model.FocusKeyphrase;
                data.SEOtitle = model.SEOtitle;
                data.Slug = model.Slug;
                data.MetaDescription = model.MetaDescription;
                data.IsCrawl = model.IsCrawl;
                data.CanonicalURL = model.CanonicalURL;
                data.SchemaTags = model.SchemaTags;
                data.isActive = true;
                _db.tblContentMasters.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("ContentMasterList");
        }

        public ActionResult isDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.tblContentMasters.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.tblContentMasters.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = true;
                _db.SaveChanges();
            }
            string status = value == "deactive" ? "active" : "deactive";
            return RedirectToAction("ContentMasterList", new { Status = status });
        }
    }
}