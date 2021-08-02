using CONSTRUCTION.Areas.Admin.Model;
using CONSTRUCTION.CommonMethods;
using CONSTRUCTION.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        CryptoMethod _cryptoMethod = new CryptoMethod();

        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            login.Password = _cryptoMethod.Encrypt(login.Password);
            var isIxists = _db.tblSuperAdmins.Where(x => x.UserName == login.UserName.ToLower() && x.Password == login.Password).FirstOrDefault();
            if (isIxists != null)
            {
                HttpCookie myCookie = new HttpCookie("Login");
                myCookie["UserName"] = isIxists.UserName;
                myCookie["UserId"] = isIxists.Id.ToString();
                Response.Cookies.Add(myCookie);
                return Json("Success",JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error",JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Login");
        }
    }
}