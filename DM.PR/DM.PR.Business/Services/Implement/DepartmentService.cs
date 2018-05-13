using DM.PR.Common.Entities;
using DM.PR.Data.Repositories;

namespace DM.PR.Business.Services.Implement
{
    public class DepartmentService : IDepartmentService
    {
        #region Private

        private IDepartmentRepository _departmentRepository;

        #endregion

        #region Ctor

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        #endregion

        #region Create

        public void Create(Department department)
        {
            _departmentRepository.Create(department);
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            _departmentRepository.Delete(id);
        }


        #endregion

        #region Edit

        public void Edit(Department department)
        {
            _departmentRepository.Update(department);
        }


        #endregion

    }
}

                                                         