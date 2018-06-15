using DM.PR.Data.SpecificationCreators;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Services;
using DM.PR.Common.Helpers;
using System;

[assembly: InternalsVisibleTo("DM.PR.Business.Test")]

namespace DM.PR.Business.Providers.Implement
{
    internal class EmployeeProvider : IEmployeeProvider
    {
        private readonly IRepository<Employee> _rep;
        private readonly IСacheStorage _caching;
        private readonly IEmployeeSpecificationCreator _specificationCreator;

        public EmployeeProvider(IRepository<Employee> rep, IEmployeeSpecificationCreator creator, IСacheStorage caching)
        {
            Inspector.ThrowExceptionIfNull(rep, creator, caching);
            _rep = rep;
            _specificationCreator = creator;
            _caching = caching;
        }

        public Employee GetById(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Employee> GetPage(int pageSize, int page, out int totalCount)
        {

            var findByPageSpecification = _specificationCreator.CreateSpecification(pageSize, page);
            return _rep.FindBy(findByPageSpecification, out totalCount);
        }

        public IReadOnlyCollection<Employee> GetPageByDepartmentId(int departmentId, int pageSize, int page, out int totalCount)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(pageSize, page);

            var findByPageAndDepartmentIdSpecification = _specificationCreator.CreateSpecification(departmentId, pageSize, page);
            return _rep.FindBy(findByPageAndDepartmentIdSpecification, out totalCount);

        }

        public IReadOnlyCollection<Employee> GetPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page, out int totalCount)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(pageSize, page, toYear);

            if (fromYear > toYear) { throw new Exception("Неверно указаны параметры поиска"); }  // уточнить

            ISpecification FindPageBySearchParamsSpecification = _specificationCreator.CreateSpecification(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, page);
            return _rep.FindBy(FindPageBySearchParamsSpecification, out totalCount);
        }
    }
}

