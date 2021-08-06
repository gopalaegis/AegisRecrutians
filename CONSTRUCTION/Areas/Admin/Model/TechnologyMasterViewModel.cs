using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CONSTRUCTION.Areas.Admin.Model
{
    public class TechnologyMasterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool isactive { get; set; }
    }
}