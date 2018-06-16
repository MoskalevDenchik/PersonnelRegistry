using System.Collections.Generic;       
using DM.PR.Common.Entities;

namespace DM.PR.WEB.Models
{
    public class DepartmentDeatailsViewModel
    {

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public IReadOnlyCollection<Phone> Phones { get; set; }     
    }
}