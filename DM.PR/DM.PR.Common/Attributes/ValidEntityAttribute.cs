using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities;
using System;

namespace DM.PR.Common.Attributes
{
    class ValidEntityAttribute : ValidationAttribute
    {
        public override bool IsValid(object entity) => ((IEntity)entity).Id >= 0 ? true : throw new Exception("Чет не так");
    }
}
