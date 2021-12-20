using CONSTRUCTION.Areas.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CONSTRUCTION.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            skillMasterViewModels = new List<SkillMasterViewModel>();
            technologyMasterViewModels = new List<TechnologyMasterViewModel>();
        }
        public List<SkillMasterViewModel> skillMasterViewModels { get; set; }
        public List<TechnologyMasterViewModel> technologyMasterViewModels { get; set; }
        public string SchemaDescription { get; set; }
    }
}