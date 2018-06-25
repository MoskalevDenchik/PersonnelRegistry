using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System;

namespace DM.PR.Common.Attributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        public string LessOrEqualTo { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = value as DateTime?;

            if (endDate != null)
            {
                DateTime beginDate = (DateTime)typeof(Employee).GetProperty(LessOrEqualTo).GetValue(validationContext.ObjectInstance);
                if (beginDate.CompareTo((DateTime)endDate) <= 0)
                {
                    return new ValidationResult(ErrorMessage, new List<string> { "EndWork" });
                }
            }
            return null;
        }

    }
}
