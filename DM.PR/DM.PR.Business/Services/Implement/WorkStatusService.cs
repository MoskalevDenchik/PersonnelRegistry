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
    internal class WorkStatusService : EntityService<WorkStatus>
    {
        #region Private

        private readonly IProvider<WorkStatus> _prov;

        #endregion

        #region Ctors

        public WorkStatusService(IRepository<WorkStatus> rep, IProvider<WorkStatus> prov) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _prov = prov;
        }

        #endregion

        protected override bool IsValid(Result result, WorkStatus wStatus)
        {
            var ms = _prov.GetAll().Where(x => x.Status == wStatus.Status).FirstOrDefault();
            if (ms == null || ms.Id == wStatus.Id)
            {
                result.Status = Status.Success;
                result.Exceptions = null;
                return true;
            }
            else
            {
                result.Status = Status.Failure;
                result.Exceptions = new List<ValidationResult> { new ValidationResult("Такой статус уже существует", new List<string> { nameof(wStatus.Status) }) };
                return false;
            }
        }
    }
}
