using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace DM.PR.WEB.Infrastructure.Attributes
{
    public class DepNullAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = "Степан привет";
                return false;
            }
            return true;
        }
    }
}