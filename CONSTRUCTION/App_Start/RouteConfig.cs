﻿using System;
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
               name: "ReferJobs",
               url: "ReferJob/{jobId}",
               defaults: new { controller = "Refer", action = "Index", jobId = UrlParameter.Optional }
           );

            routes.MapRoute(
                 name: "ApplyJobs",
                 url: "ApplyJob/{jobId}",
                 defaults: new { controller = "ApplyJob", action = "Index", jobId = UrlParameter.Optional }
             );


            routes.MapRoute(
              name: "bothSearchJobs",
              url: "{q}-Jobs-in-{l}",
              defaults: new { controller = "jobs", action = "Index" }
          );

            routes.MapRoute(
              name: "SearchJobsbytitle",
              url: "{q}-Jobs",
              defaults: new { controller = "jobs", action = "Index" }
          );

            routes.MapRoute(
              name: "SearchJobsbyCity",
              url: "Jobs-in-{l}",
              defaults: new { controller = "jobs", action = "Index" }
          );

            routes.MapRoute(
               name: "SearchJobs",
               url: "Jobs",
               defaults: new { controller = "jobs", action = "Index" }
           );

            routes.MapRoute(
                name: "SlugURL",
                url: "{slugURL}",
                defaults: new { controller = "jobs", action = "TechnoloGyWiseJob" }
            );
         

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}
