using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Common.Attributes
{
    public class EmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value))
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