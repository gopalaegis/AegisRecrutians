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
    public class ContentMasterController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Admin/ContentMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditContentMaster(int Id = 0)
        {
            ContentMasterViewModel model = new ContentMasterViewModel();
            if (Id > 0)
            {
                model = _db.tblContentMasters.Where(x => x.Id == Id).Select(x => new ContentMasterViewModel { Id = x.Id, Title = x.Title, Controller = x.Controller, ActionResult = x.ActionResult, FocusKeyphrase = x.FocusKeyphrase, SEOtitle = x.SEOtitle, Slug = x.Slug, MetaDescription = x.MetaDescription, IsCrawl = (bool)x.IsCrawl, CanonicalURL = x.CanonicalURL, SchemaTags = x.SchemaTags }).FirstOrDefault();
            }
            return PartialView("_partialAddContentMaster", model);
        }

        public ActionResult ContentMasterList()
        {
            List<ContentMasterViewModel> model = new List<ContentMasterViewModel>();
            model = _db.tblContentMasters.Select(x => new ContentMasterViewModel { Id = x.Id, Title = x.Title, Controller = x.Controller,ActionResult=x.ActionResult,isactive=(bool)x.isActive }).ToList();
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
            return RedirectToAction("ContentMasterList");
        }
    }
}