using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class jobsController : Controller
    {
        // GET: jobs
        public ActionResult Index()
        {
            return View();
        }
    }
}