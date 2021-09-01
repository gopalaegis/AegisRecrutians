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
    public class TechnologyMasterController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        CommonMethod _commonMethod = new CommonMethod();
        // GET: Admin/TechnologyMaster
        [UserLoginCheck]
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
                    model.BriefDescription = data.BriefDescription;
                    model.IsCrawl = (bool)data.IsCrawl;
                    model.CanonicalURL = data.CanonicalURL;
                    model.SchemaTags = data.SchemaTags;
                }
            }
            return PartialView("_partialAddTechnologyMaster", model);
        }

        public ActionResult AddCityMaster(int Id = 0)
        {
            AddcityViewModel model = new AddcityViewModel();
            model.teckmasterId = Id;
            model.id = 0;
            return PartialView("_partialCityMaster", model);
        }

        public ActionResult EditCityMaster(int id = 0,int teckId=0)
        {
            AddcityViewModel model = new AddcityViewModel();
            if (id > 0)
            {
                var data = _db.tblAddCityTechMasters.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.id = data.Id;
                    model.cityId = (int)data.CityId;
                    model.drpcity = _commonMethod.GetCity();
                    model.teckmasterId = (int)data.TechMasterId;
                    model.FocusKeyphrase = data.FocusKeyphrase;
                    model.SEOtitle = data.SEOtitle;
                    model.Slug = data.Slug;
                    model.MetaDescription = data.MetaDescription;
                    model.BriefDescription = data.BriefDescription;
                    model.IsCrawl = (bool)data.IsCrawl;
                    model.CanonicalURL = data.CanonicalURL;
                    model.SchemaTags = data.SchemaTags;
                    model.isactive = (bool)data.isActive;
                }
            }
            else
            {
                 model.drpcity = _commonMethod.GetCity();
                model.teckmasterId = teckId;
            }
            return PartialView("_partialEditCityMaster", model);
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
                m.isactive = Convert.ToBoolean(item.isActive);
                m.Slug = item.Slug;
                m.SEOtitle = item.SEOtitle;
                model.Add(m);
            }
            return PartialView("_partialTechnologyMasterList", model);
        }
        public ActionResult TechnologyCitylist(int Id=0)
        {
            List<AddcityViewModel> model = new List<AddcityViewModel>();
            var data = _db.tblAddCityTechMasters.Where(x=>x.TechMasterId == Id).ToList();
            foreach (var item in data)
            {
                AddcityViewModel m = new AddcityViewModel();
                m.id = item.Id;
                m.teckmasterId = (int)item.TechMasterId;
                m.city = _db.tblCities.Where(x => x.Id == item.CityId).Select(x => x.Name).FirstOrDefault();
                m.FocusKeyphrase = item.FocusKeyphrase;
                m.SEOtitle = item.SEOtitle;
                m.Slug = item.Slug;
                m.IsCrawl = (bool)item.IsCrawl;
                m.CanonicalURL = item.CanonicalURL;
                m.SchemaTags = item.SchemaTags;
                m.isactive = Convert.ToBoolean(item.isActive);
                model.Add(m);
            }
            return PartialView("_partialTechnologyCityList", model);
        }

        public ActionResult SaveCityMaster(AddcityViewModel model)
        {
            if (model.id > 0)
            {
                var data = _db.tblAddCityTechMasters.Where(x => x.Id == model.id).FirstOrDefault();
                data.CityId = model.cityId;
                data.FocusKeyphrase = model.FocusKeyphrase;
                data.SEOtitle = model.SEOtitle;
                data.Slug = model.Slug;
                data.MetaDescription = model.MetaDescription;
                data.IsCrawl = model.IsCrawl;
                data.CanonicalURL = model.CanonicalURL;
                data.SchemaTags = model.SchemaTags;
                data.BriefDescription = model.BriefDescription;
                _db.SaveChanges();
            }
            else
            {
                tblAddCityTechMaster data = new tblAddCityTechMaster();
                data.CityId = model.cityId;
                data.TechMasterId = model.teckmasterId;
                data.FocusKeyphrase = model.FocusKeyphrase;
                data.SEOtitle = model.SEOtitle;
                data.Slug = model.Slug;
                data.MetaDescription = model.MetaDescription;
                data.IsCrawl = model.IsCrawl;
                data.CanonicalURL = model.CanonicalURL;
                data.SchemaTags = model.SchemaTags;
                data.isActive = true;
                data.BriefDescription = model.BriefDescription;
                _db.tblAddCityTechMasters.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("AddCityMaster",new{ Id = model.teckmasterId });
        }
        public ActionResult DeletecityMaster(int Id)
        {
            var data = _db.tblAddCityTechMasters.Where(x => x.Id == Id).FirstOrDefault();
            int teckid = (int)data.TechMasterId;
            _db.tblAddCityTechMasters.Remove(data);
            _db.SaveChanges();
            return RedirectToAction("TechnologyCitylist", new { Id = teckid });
        }

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
                data.BriefDescription = model.BriefDescription;
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
                data.BriefDescription = model.BriefDescription;
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

        //public ActionResult isDeactiveCity(int Id, string value)
        //{
        //    if (value == "deactive")
        //    {
        //        var record = _db.tblAddCityTechMasters.Where(x => x.Id == Id).FirstOrDefault();
        //        record.isActive = false;
        //        _db.SaveChanges();
        //    }
        //    else
        //    {
        //        var record = _db.tblAddCityTechMasters.Where(x => x.Id == Id).FirstOrDefault();
        //        record.isActive = true;
        //        _db.SaveChanges();
        //    }
        //    return RedirectToAction("TechnologyMasterList");
        //}
    }
}