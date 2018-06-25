using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class DepartmentService : EntityService<Department>
    {
        #region Private

        private readonly IDepartmentProvider _prov;

        #endregion

        #region Ctors

        public DepartmentService(IRepository<Department> rep, IDepartmentProvider prov) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _prov = prov;
        }

        #endregion

        protected override bool IsValid(Result result, Department department)
        {
            var dep = _prov.GetByName(department.Name);

            if (dep == null || dep.Id == department.Id)
            {
                result.Status = Status.Success;
                result.Exceptions = null;
                return true;
            }
            else
            {
                result.Status = Status.Failure;
                result.Exceptions = new List<ValidationResult> { new ValidationResult("Отдел с таким именем уже существует", new List<string> { nameof(department.Name) }) };
                return false;
            }
        }
    }
}
