using System.Collections.Generic;

namespace DM.PR.Common.Entities
{
    public class Department
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<Phone> Phones { get; set; }
    }
}
