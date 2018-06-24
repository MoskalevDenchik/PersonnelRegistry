using DM.PR.Business.Helpers;
using DM.PR.Business.Providers;
using DM.PR.Common.Entities;
using DM.PR.Common.Entities.Account;
using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.PR.Business.Services.Implement
{
    internal class UserService : EntityService<User>
    {

        #region Private

        private readonly IRepository<User> _rep;
        private readonly IUserProvider _prov;
        private readonly EntityValidator<User> _validator;

        #endregion

        #region Ctors

        public UserService(IRepository<User> rep, IUserProvider provider) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _validator = new EntityValidator<User>();
            _rep = rep;
            _prov = provider;
        }

        #endregion


        public override Result Save(User entity)
        {
            var result = _validator.Validate(entity);

            if (result.Status == Status.Success)
            {
                var user = _prov.GetByLogin(entity.Login);
                if (user == null)
                {
                    _rep.Save(entity);
                }
                else
                {
                    result.Status = Status.InValid;
                    result.Exceptions = new List<ValidationResult> { new ValidationResult("Пользователь с таким именем уже существует", new List<string> { nameof(entity.Login) }) };
                }
            }

            return result;
        }

    }
}
