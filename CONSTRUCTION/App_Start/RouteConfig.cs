using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CONSTRUCTION
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
           name: "JobsList",
           url: "jobs/List",
           defaults: new { controller = "jobs", action = "List"}
       );

            routes.MapRoute(
            name: "Jobs",
            url: "jobs/{city}",
            defaults: new { controller = "jobs", action = "Index", city = UrlParameter.Optional }
        );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            //  routes.MapRoute(
            //    name: "JobTechnology",
            //    url: "{controller}/{TechnologyId}",
            //    defaults: new { controller = "jobs", action = "JobMain", TechnologyId = UrlParameter.Optional }
            //);

         //   routes.MapRoute(
         //    name: "JobList",
         //    url: "{controller}/{City}/{Title}/{Technology}",
         //    defaults: new { controller = "jobs", action = "Index", City = UrlParameter.Optional, Title = UrlParameter.Optional, Technology = UrlParameter.Optional }
         //);
        }
    }
}
