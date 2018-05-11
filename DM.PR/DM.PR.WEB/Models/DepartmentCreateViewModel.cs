using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.WEB.Models
{
    public class DepartmentCreateViewModel
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<Phone> Phones { get; set; }
    }
}