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
             name: "BlogDetail",
             url: "blog/{slugURL}",
             defaults: new { controller = "Blog", action = "BlogDetail" }
         );

            routes.MapRoute(
              name: "aboutUs",
              url: "about-us",
              defaults: new { controller = "aboutUs", action = "Index" }
          );

            routes.MapRoute(
             name: "Professionals",
             url: "professionals",
             defaults: new { controller = "Professionals", action = "Index" }
         );

            routes.MapRoute(
            name: "Student",
            url: "students-graduates",
            defaults: new { controller = "Student", action = "Index" }
        );

            routes.MapRoute(
           name: "Blog",
           url: "blog",
           defaults: new { controller = "Blog", action = "Index" }
       );

       routes.MapRoute(
             name: "Articles",
             url: "articles",
             defaults: new { controller = "Articles", action = "Index" }
         );

            routes.MapRoute(
           name: "SaveDataJob",
           url: "ApplyJob/SaveData",
           defaults: new { controller = "ApplyJob", action = "SaveData" }
       );

            routes.MapRoute(
            name: "GetStateByCountry",
            url: "ApplyJob/GetStateByCountry",
            defaults: new { controller = "ApplyJob", action = "GetStateByCountry" }
        );

            routes.MapRoute(
         name: "ExperienceData",
         url: "ApplyJob/ExperienceData",
         defaults: new { controller = "ApplyJob", action = "ExperienceData" }
     );

            routes.MapRoute(
         name: "EducationData",
         url: "ApplyJob/EducationData",
         defaults: new { controller = "ApplyJob", action = "EducationData" }
     );

            routes.MapRoute(
             name: "ApplyjobresumeUpload",
             url: "ApplyJob/FileUplaod",
             defaults: new { controller = "ApplyJob", action = "FileUplaod" }
         );

            routes.MapRoute(
               name: "ReferJobs",
               url: "referjob/{jobId}",
               defaults: new { controller = "Refer", action = "Index", jobId = UrlParameter.Optional }
           );

            routes.MapRoute(
                 name: "ApplyJobs",
                 url: "applyjob/{jobId}",
                 defaults: new { controller = "ApplyJob", action = "Index", jobId = UrlParameter.Optional }
             );

            routes.MapRoute(
              name: "SlugURL",
              url: "{slugURL}",
              defaults: new { controller = "jobs", action = "TechnoloGyWiseJob" }
          );

            routes.MapRoute(
              name: "bothSearchJobs",
              url: "job/{q}-jobs-in-{l}",
              defaults: new { controller = "jobs", action = "Index" }
          );

            routes.MapRoute(
              name: "SearchJobsbytitle",
              url: "job/{q}-jobs",
              defaults: new { controller = "jobs", action = "Index" }
          );

            routes.MapRoute(
              name: "SearchJobsbyCity",
              url: "job/jobs-in-{l}",
              defaults: new { controller = "jobs", action = "Index" }
          );

            routes.MapRoute(
               name: "SearchJobs",
               url: "job/alljob",
               defaults: new { controller = "jobs", action = "Index" }
           );

          
         

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}
