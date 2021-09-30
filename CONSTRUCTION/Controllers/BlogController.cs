using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class BlogController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Blog
        public ActionResult Index()
        {
            SchemaTagModel model = new SchemaTagModel();
            var schematag = _db.Schematag_master.Where(x => x.SchemaTag == "professionals").FirstOrDefault();
            model.Description = schematag != null ? schematag.description : "";
            return View(model);
        }
    }
}