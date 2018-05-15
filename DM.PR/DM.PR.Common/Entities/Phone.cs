using DM.PR.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Common.Entities
{
    public class Phone
    {
        public int? Id { get; set; }
        public string Number { get; set; }
        public KindOfPhone Kind { get; set; }
    }
}
