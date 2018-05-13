using DM.PR.Common.Entities;
using DM.PR.Data.Repositories;
using System.Collections.Generic;

namespace DM.PR.Business.Providers.Implement
{
    public class DepartmentProvider : IDepartmentProvider
    {
        #region Private

        private IDepartmentRepository _departmentRepository;

        #endregion

        #region Ctor

        public DepartmentProvider(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        #endregion

        #region GetAll

        public IReadOnlyCollection<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        #endregion

        #region GetById

        public Department GetById(int id)
        {
            return _departmentRepository.GetById(id);
        }

        #endregion

    }
}
