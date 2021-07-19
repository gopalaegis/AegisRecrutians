using CONSTRUCTION.Areas.Admin.Model;
using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class HomeController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.skillMasterViewModels = _db.tblSkillMasters.Select(x => new SkillMasterViewModel { Id = x.Id, Name = x.Name }).ToList();
            model.technologyMasterViewModels = _db.tblTechnologyMasters.Select(x => new TechnologyMasterViewModel { Id = x.Id, Name = x.Name, Image = x.Image }).ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}