using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.PR.Common.Entities
{
    public class Result
    {
        public Status Status { get; set; }

        public List<ValidationResult> Exceptions { get; set; }
    }
}
