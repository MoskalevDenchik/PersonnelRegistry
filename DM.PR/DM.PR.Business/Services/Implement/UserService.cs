using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class UserService : EntityService<User>
    {

        #region Private

        private readonly IUserProvider _prov;

        #endregion

        #region Ctors

        public UserService(IRepository<User> rep, IUserProvider prov) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _prov = prov;
        }

        #endregion

        protected override bool IsValid(Result result, User user)
        {
            var us = _prov.GetByLogin(user.Login);
            if (us == null || us.Id == user.Id)
            {
                result.Exceptions = null;
                result.Status = Status.Success;
                return true;
            }
            else
            {
                result.Status = Status.Failure;
                result.Exceptions = new List<ValidationResult> { new ValidationResult("Пользователь с таким именем уже существует", new List<string> { nameof(user.Login) }) };
                return false;
            }
        }
    }
}
