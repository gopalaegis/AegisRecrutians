using CONSTRUCTION.CommonMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        [UserLoginCheck]
        public ActionResult Index()
        {
            return View();
        }
    }
}