using System;
using System.ComponentModel.DataAnnotations;    

namespace DM.PR.Common.Attributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            //DateTime date = value as DateTime;

            return base.IsValid(value, validationContext);
        }

    }
}
