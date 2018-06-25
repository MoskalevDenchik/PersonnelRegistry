using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using System.Linq;

namespace DM.PR.Common.Attributes
{
    public class UniqRolesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Role> email = value as List<Role>;

            var groups = email.GroupBy(x => x.Id);
            foreach (var item in groups)
            {
                if (item.Count() > 1)
                {
                    for (int i = (email.Count - 1); i > 0; i--)
                    {
                        if (email[i].Id == item.Key)
                        {
                            return new ValidationResult(ErrorMessage, new List<string> { $"Roles[{i}].Id" });
                        }
                    }
                }
            }
            return null;
        }
    }
}
