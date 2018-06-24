using System.ComponentModel.DataAnnotations;       
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.Business.Helpers
{
    public class EntityValidator<T>
    {
        public Result Validate(T entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                return new Result { Exceptions = results, Status = Status.InValid };
            }
            else
            {
                return new Result { Status = Status.Success };
            }
        }
    }
}
