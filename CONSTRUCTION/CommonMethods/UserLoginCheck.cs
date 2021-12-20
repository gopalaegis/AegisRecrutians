using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.CommonMethods
{
    public class UserLoginCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.Request.Cookies["Login"];
            if (user == null)
                filterContext.Result = new RedirectResult(string.Format("/Admin/Login/Index?targetUrl={0}", filterContext.HttpContext.Request.Url.AbsolutePath));
        }
    }
}