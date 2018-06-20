using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using System.Linq;

namespace DM.PR.Common.Attributes
{
    public class RolesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<Role> roles = value as List<Role>;

            if (roles == null || roles.Count() == 0)
            {
                ErrorMessage = "У пользователя должна быть хоть одна роль";
                return false;
            }
            else
            {
                //проверить на повторение
            }
            return true;
        }

    }
}
