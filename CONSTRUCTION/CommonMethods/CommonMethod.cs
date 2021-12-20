using CONSTRUCTION.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONSTRUCTION.CommonMethods
{
    public class CommonMethod
    {
        AEGIS_Entities _db = new AEGIS_Entities();

        public List<SelectListItem> GetJobEducation()
        {
            var data = _db.tblJobEducations.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Degree" });
            foreach (var item in data)
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Education });
            }
            return list;
        }

        public List<SelectListItem> GetCountry()
        {
            var data = _db.tblCountries.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Country" });
            foreach (var item in data)
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            return list;
        }

        public List<SelectListItem> GetStateByCountry(int CountryId)
        {
            var data = _db.tblStates.Where(x=>x.CountryId==CountryId).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "State" });
            foreach (var item in data)
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            return list;
        }

        public List<SelectListItem> GetCity()
        {
            var data = _db.tblCities.ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "City" });
            foreach (var item in data.OrderBy(x=>x.Name).ToList())
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            return list;
        }
    }
}