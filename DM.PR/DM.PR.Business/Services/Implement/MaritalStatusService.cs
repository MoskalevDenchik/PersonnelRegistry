using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Linq;

namespace DM.PR.Business.Services.Implement
{
    internal class MaritalStatusService : EntityService<MaritalStatus>
    {
        #region Private

        private readonly IProvider<MaritalStatus> _prov;

        #endregion

        #region Ctors

        public MaritalStatusService(IRepository<MaritalStatus> rep, IProvider<MaritalStatus> prov) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _prov = prov;
        }

        #endregion

        protected override bool IsValid(Result result, MaritalStatus maritalStatus)
        {
            var ms = _prov.GetAll().Where(x => x.Status == maritalStatus.Status).FirstOrDefault();
            if (ms == null || ms.Id == maritalStatus.Id)
            {
                result.Status = Status.Success;
                result.Exceptions = null;
                return true;
            }
            else
            {
                result.Status = Status.Failure;
                result.Exceptions = new List<ValidationResult> { new ValidationResult("Такой статус уже существует", new List<string> { nameof(maritalStatus.Status) }) };
                return false;
            }
        }
    }
}
