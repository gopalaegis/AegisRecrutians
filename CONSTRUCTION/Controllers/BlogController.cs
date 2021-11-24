using CONSTRUCTION.DataTable;
using CONSTRUCTION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.Controllers
{
    public class BlogController : Controller
    {
        AEGIS_Entities _db = new AEGIS_Entities();
        // GET: Blog
        public ActionResult Index(int CategoryId = 0)
        {
            SchemaTagModel model = new SchemaTagModel();
            var schematag = _db.Schematag_master.Where(x => x.SchemaTag == "blog").FirstOrDefault();
            model.Description = schematag != null ? schematag.description : "";
            Tuple<SchemaTagModel, int> tuple = new Tuple<SchemaTagModel, int>(model, CategoryId);
            return View(tuple);
        }

        public ActionResult BindBlog(int CategoryId = 0)
        {
            var blogData = _db.Blogs.Where(x => x.isActive == true).ToList();
            List<BlogListViewModel> model = new List<BlogListViewModel>();
            foreach (var item in blogData)
            {
                BlogListViewModel m = new BlogListViewModel();
                m.Id = item.Id;
                m.CategoryId = Convert.ToInt32(item.CategoryId);
                m.CategoryName = _db.BlogCategories.Where(x => x.Id == item.CategoryId).FirstOrDefault().Category;
                m.Date = item.Date;
                m.Image = item.Image;
                m.BlogTitle = item.BlogTitle;
                m.ShortDescription = item.ShortDescription;
                m.Blogdetail = item.Blogdetail;
                m.URL = item.URL;
                m.MetaTags = item.MetaTags;
                model.Add(m);
            }
            if (CategoryId > 0)
            {
                model = model.Where(x => x.CategoryId == CategoryId).ToList();
            }
            return PartialView("_partialBLogList", model);
        }

        public ActionResult BlogDetail(string slugURL = "")
        {
            BlogListViewModel model = new BlogListViewModel();
            if (!string.IsNullOrEmpty(slugURL))
            {
                var blogData = _db.Blogs.Where(x => x.URL == slugURL).FirstOrDefault();
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
                }
            }
            List<SelectListItem> categoryList = new List<SelectListItem>();

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

            List<SelectListItem> blogList = new List<SelectListItem>();
            var blogDataList = _db.Blogs.Where(x => x.isActive == true).OrderByDescending(x => x.Id).Take(5).ToList();
            foreach (var category in blogDataList)
            {
                if (!string.IsNullOrEmpty(category.URL) && !string.IsNullOrEmpty(category.URL))
                {
                    SelectListItem selectListItem = new SelectListItem()
                    {
                        Text = category.BlogTitle,
                        Value = category.URL.ToString()
                    };
                    blogList.Add(selectListItem);
                }
            }
            model.BlogList = blogList;
            return View(model);
        }
        public ActionResult BindHTMLDetail(int Id)
        {
            string htmlString = "";

            var technologyNameCity = _db.Blogs.Where(x => x.Id == Id).FirstOrDefault();
            if (technologyNameCity != null)
            {
                if (!string.IsNullOrEmpty(technologyNameCity.Blogdetail))
                {
                    var c = new HtmlString(technologyNameCity.ToString());
                    htmlString = Regex.Replace(technologyNameCity.Blogdetail, "(?<=\\<[^<>]*)\"(?=[^><]*\\>)", "'");
                }
            }
            SEOModel seo = new SEOModel();
            seo.jobDescription = htmlString;
            return View(seo);
        }

    }
}