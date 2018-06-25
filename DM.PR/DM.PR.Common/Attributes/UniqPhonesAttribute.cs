using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using DM.PR.Common.Entities;

namespace DM.PR.Common.Attributes
{
    public class UniqPhonesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Phone> email = value as List<Phone>;

            var groups = email.GroupBy(x => x.Number);
            foreach (var item in groups)
            {
                if (item.Count() > 1)
                {
                    for (int i = (email.Count - 1); i > 0; i--)
                    {
                        if (email[i].Number == item.Key)
                        {
                            return new ValidationResult(ErrorMessage, new List<string> { $"Phones[{i}].Number" });
                        }
                    }
                }
            }
            return null;
        }
    }
}
