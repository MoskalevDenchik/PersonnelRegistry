using DM.PR.Data.SpecificationCreators;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

[assembly: InternalsVisibleTo("DM.PR.Business.Test")]

namespace DM.PR.Business.Providers.Implement
{
    internal class EmployeeProvider : IEmployeeProvider
    {
        private readonly IRepository<Employee> _rep;
        private readonly IEmployeeSpecificationCreator _specificationCreator;

        public EmployeeProvider(IRepository<Employee> rep, IEmployeeSpecificationCreator creator)
        {
            Inspector.ThrowExceptionIfNull(rep, creator);
            _specificationCreator = creator;
            _rep = rep;
        }

        public Employee GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Employee> GetEmployees(int pageSize, int page, out int totalCount)
        {
            if (pageSize <= 0 || page <= 0)
            {
                totalCount = 0;
                return null;
            }

            ISpecification specification = _specificationCreator.CreateSpecification(pageSize, page);
            return _rep.FindBy(specification, out totalCount);
        }

        public IReadOnlyCollection<Employee> GetEmployees(int departmentId, int pageSize, int page, out int totalCount)
        {
            if (page <= 0 || page <= 0 || departmentId < 0)
            {
                totalCount = 0;
                return null;
            }

            ISpecification specification = _specificationCreator.CreateSpecification(departmentId, pageSize, page);
            return _rep.FindBy(specification, out totalCount);

        }

        public IReadOnlyCollection<Employee> GetEmloyees(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page, out int totalCount)
        {
            if (page <= 0 || page <= 0 || fromYear < 0 || toYear < 0)
            {
                totalCount = 0;
                return null;
            }

            ISpecification specification = _specificationCreator.CreateSpecification(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, page);
            return _rep.FindBy(specification, out totalCount);
        }
    }
}

