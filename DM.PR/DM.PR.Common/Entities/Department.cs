using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Common.Entities
{
    public class Department
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<Phone> Phones { get; set; }
    }
}
