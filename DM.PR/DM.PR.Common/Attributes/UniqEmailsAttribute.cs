using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Linq;

namespace DM.PR.WEB.Infrastructure.Attributes
{
    public class UniqEmailsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Email> email = value as List<Email>;

            var groups = email.GroupBy(x => x.Address);
            foreach (var item in groups)
            {
                if (item.Count() > 1)
                {
                    for (int i = (email.Count - 1); i > 0; i--)
                    {
                        if (email[i].Address == item.Key)
                        {
                            return new ValidationResult(ErrorMessage, new List<string> { $"Emails[{i}].Address" });
                        }
                    }
                }
            }
            return null;
        }
    }
}