using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.Common.Attributes
{
    public class EmailsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Email> email = value as List<Email>;

            List<string> names = new List<string>();
            string errorMessage = "Поле не должно быть пустым";

            for (int i = 0; i < email.Count; i++)
            {
                if (string.IsNullOrEmpty(email[i].Address))
                {
                    names.Add($"Emails[{i}].Address");
                }
            }

            return new ValidationResult(errorMessage, names);
        }
    }
}