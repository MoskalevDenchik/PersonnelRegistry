using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.PR.WEB.Models
{
    public class NavDepartmentViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<NavDepartmentViewModel> Children { get; set; }
        public NavDepartmentViewModel Parent { get; set; }
    }
}