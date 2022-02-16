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
    public class BlogDetailController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Admin/SkillMaster
        [UserLoginCheck]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEditBlogCategory(int Id = 0)
        {
            BlogCategoryViewModel model = new BlogCategoryViewModel();
            if (Id > 0)
            {
                model = _db.BlogCategories.Where(x => x.Id == Id).Select(x => new BlogCategoryViewModel { Id = x.Id, Name = x.Category }).FirstOrDefault();
            }
            return PartialView("_partialAdd", model);
        }

        public ActionResult BlogCategoryList(string Status = "active")
        {
            List<BlogCategoryViewModel> model = new List<BlogCategoryViewModel>();
            model = _db.BlogCategories.Select(x => new BlogCategoryViewModel { Id = x.Id, Name = x.Category, isactive = (bool)x.IsActive }).ToList();
            if (Status == "active")
            {
                model = model.Where(x => x.isactive == true).ToList();
            }
            else
            {
                model = model.Where(x => x.isactive == false).ToList();
            }
            return PartialView("_partialList", model);
        }



        [HttpPost]
        public ActionResult SaveBlogMaster(BlogCategoryViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.BlogCategories.Where(x => x.Id == model.Id).FirstOrDefault();
                data.Category = model.Name;
                _db.SaveChanges();
            }
            else
            {
                BlogCategory data = new BlogCategory();
                data.Category = model.Name;
                data.IsActive = true;
                _db.BlogCategories.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("BlogCategoryList");
        }

        public ActionResult isDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.BlogCategories.Where(x => x.Id == Id).FirstOrDefault();
                record.IsActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.BlogCategories.Where(x => x.Id == Id).FirstOrDefault();
                record.IsActive = true;
                _db.SaveChanges();
            }

            string status = value == "deactive" ? "active" : "deactive";
            return RedirectToAction("BlogCategoryList", new { Status = status });
        }

        //blog

        [UserLoginCheck]
        public ActionResult Blog()
        {
            return View();
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
                uploadFile.SaveAs(Server.MapPath("~/CommonImage/Blog/") + fileName);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEditBlog(int Id = 0)
        {
            BlogViewModel model = new BlogViewModel();
            if (Id > 0)
            {
                var blogData = _db.Blogs.Where(x => x.Id == Id).FirstOrDefault();
                if (blogData != null)
                {
                    model.Id = blogData.Id;
                    model.CategoryId = Convert.ToInt32(blogData.CategoryId);
                    model.CategoryName = _db.BlogCategories.Where(x => x.Id == model.CategoryId).FirstOrDefault().Category;
                    model.Date = blogData.Date;
                    model.Image = blogData.Image;
                    model.BlogTitle = blogData.BlogTitle;
                    model.ShortDescription = blogData.ShortDescription;
                    model.Blogdetail = blogData.Blogdetail;
                    model.SEOTitle = blogData.SEOTitle;
                    model.URL = blogData.URL;
                    model.SEODescription = blogData.SEODescription;
                    model.MetaTags = blogData.MetaTags;
                    model.IsCrawl = Convert.ToBoolean(blogData.IsCrawl);
                    model.ConicalURL = blogData.ConicalURL;
                }
            }
            List<SelectListItem> categoryList = new List<SelectListItem>();
            SelectListItem select = new SelectListItem()
            {
                Text = "Select Category",
                Value = "0"
            };
            categoryList.Add(select);
            foreach (var category in _db.BlogCategories.Where(x => x.IsActive == true).ToList())
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = category.Category,
                    Value = category.Id.ToString()
                };
                categoryList.Add(selectListItem);
            }
            model.CategoryList = categoryList;
            return PartialView("_partialAddBlog", model);
        }



        public ActionResult BlogList(string Status = "active")
        {
            List<BlogViewModel> listModel = new List<BlogViewModel>();
            var data = _db.Blogs.ToList();
            foreach (var blogData in data)
            {
                BlogViewModel model = new BlogViewModel();
                model.Id = blogData.Id;
                model.CategoryId = Convert.ToInt32(blogData.CategoryId);
                model.CategoryName = _db.BlogCategories.Where(x => x.Id == model.CategoryId).FirstOrDefault().Category;
                model.Date = blogData.Date;
                model.Image = blogData.Image;
                model.BlogTitle = blogData.BlogTitle;
                model.ShortDescription = blogData.ShortDescription;
                model.Blogdetail = blogData.Blogdetail;
                model.SEOTitle = blogData.SEOTitle;
                model.URL = blogData.URL;
                model.SEODescription = blogData.SEODescription;
                model.MetaTags = blogData.MetaTags;
                model.IsCrawl = Convert.ToBoolean(blogData.IsCrawl);
                model.isactive = Convert.ToBoolean(blogData.isActive);
                model.ConicalURL = blogData.ConicalURL;
                listModel.Add(model);
            }
            if (Status == "active")
            {
                listModel = listModel.Where(x => x.isactive == true).ToList();
            }
            else
            {
                listModel = listModel.Where(x => x.isactive == false).ToList();
            }
            return PartialView("_partialBlogList", listModel);
        }



        [HttpPost]
        public ActionResult SaveMaster(BlogViewModel model)
        {
            if (model.Id > 0)
            {
                var data = _db.Blogs.Where(x => x.Id == model.Id).FirstOrDefault();
                data.CategoryId = model.CategoryId;
                data.Date = model.Date;
                data.Image = model.Image;
                data.BlogTitle = model.BlogTitle;
                data.ShortDescription = model.ShortDescription;
                data.Blogdetail = model.Blogdetail;
                data.SEOTitle = model.SEOTitle;
                data.URL = model.URL;
                data.SEODescription = model.SEODescription;
                data.MetaTags = model.MetaTags;
                data.ConicalURL = model.ConicalURL;
                data.IsCrawl = Convert.ToBoolean(model.IsCrawl);
                _db.SaveChanges();
            }
            else
            {
                Blog data = new DataTable.Blog();
                data.CategoryId = model.CategoryId;
                data.Date = model.Date;
                data.Image = model.Image;
                data.BlogTitle = model.BlogTitle;
                data.ShortDescription = model.ShortDescription;
                data.Blogdetail = model.Blogdetail;
                data.SEOTitle = model.SEOTitle;
                data.URL = model.URL;
                data.SEODescription = model.SEODescription;
                data.MetaTags = model.MetaTags;
                data.IsCrawl = Convert.ToBoolean(model.IsCrawl);
                data.isActive = true;
                data.ConicalURL = model.ConicalURL;
                _db.Blogs.Add(data);
                _db.SaveChanges();
            }
            return RedirectToAction("BlogList");
        }

        public ActionResult isBlogDeactive(int Id, string value)
        {
            if (value == "deactive")
            {
                var record = _db.Blogs.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = false;
                _db.SaveChanges();
            }
            else
            {
                var record = _db.Blogs.Where(x => x.Id == Id).FirstOrDefault();
                record.isActive = true;
                _db.SaveChanges();
            }

            string status = value == "deactive" ? "active" : "deactive";
            return RedirectToAction("BlogList", new { Status = status });
        }
    }
}