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
    internal class EmployeeProvider : Provider<Employee>, IEmployeeProvider
    {
        private readonly IRepository<Employee> _rep;
        private readonly IEmployeeSpecificationCreator _specificationCreator;

        public EmployeeProvider(IRepository<Employee> rep, IEmployeeSpecificationCreator creator) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep, creator);
            _specificationCreator = creator;
            _rep = rep;
        }

        public IReadOnlyCollection<Employee> GetEmployees(int pageSize, int pageNumber, out int totalCount)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                totalCount = 0;
                return null;
            }

            ISpecification specification = _specificationCreator.CreateSpecification(pageSize, pageNumber);
            return _rep.FindBy(specification, out totalCount);
        }

        public IReadOnlyCollection<Employee> GetEmployees(int departmentId, int pageSize, int pageNumber, out int totalCount)
        {
            if (pageNumber <= 0 || pageNumber <= 0 || departmentId < 0)
            {
                totalCount = 0;
                return null;
            }

            ISpecification specification = _specificationCreator.CreateSpecification(departmentId, pageSize, pageNumber);
            return _rep.FindBy(specification, out totalCount);
        }

        public IReadOnlyCollection<Employee> GetEmployees(string lastName, string firstName, string middleName, int fromYear, int toYear, int workStatusId, int pageSize, int pageNumber, out int totalCount)
        {
            if (pageSize <= 0 || pageNumber <= 0 || fromYear < 0 || toYear < 0 || workStatusId < 0)
            {
                totalCount = 0;
                return null;
            }                   

            ISpecification specification = _specificationCreator.CreateSpecification(lastName, firstName, middleName, fromYear, toYear, workStatusId, pageSize, pageNumber);
            return _rep.FindBy(specification, out totalCount);
        }
    }
}

