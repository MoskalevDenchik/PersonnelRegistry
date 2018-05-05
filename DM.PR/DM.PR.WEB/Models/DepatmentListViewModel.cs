using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.PR.WEB.Models
{
    public class DepartmentListViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Phone> Phones { get; set; }
        public string Description { get; set; }
        public int EmployeeQuantity { get; set; }
    }
}