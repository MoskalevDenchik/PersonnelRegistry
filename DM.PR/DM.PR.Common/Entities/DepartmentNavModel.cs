using System.Collections.Generic;        

namespace DM.PR.Common.Entities
{
    public class DepartmentNavModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<DepartmentNavModel> Cildren { get; set; }
      
    }
}