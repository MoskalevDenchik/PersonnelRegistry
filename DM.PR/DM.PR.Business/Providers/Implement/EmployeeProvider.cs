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
            Inspector.ThrowExceptionIfZeroOrNegative(pageSize, page);

            var cachKey = $"Employees_{pageSize}_{page}";
            var pageData = _caching.Get<PagedData<Employee>>(cachKey);
            if (pageData == null)
            {
                var findByPageSpecification = _specificationCreator.CreateFindByPageDataSpecification(pageSize, page);
                var list = _rep.FindBy(findByPageSpecification, out totalCount);
                _caching.Add(cachKey, new PagedData<Employee>(list, totalCount), 20);
                return list;
            }

            totalCount = pageData.TotalCount;
            return pageData.Data;
        }

        public IReadOnlyCollection<Employee> GetPageByDepartmentId(int departmentId, int pageSize, int page, out int totalCount)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(pageSize, page);

            var cachKey = $"Employees_{pageSize}_{page}_{departmentId}";
            var pageData = _caching.Get<PagedData<Employee>>(cachKey);
            if (pageData == null)
            {
                var findByPageAndDepartmentIdSpecification = _specificationCreator.CreateFindPageByDepartmentIdSpecification(departmentId, pageSize, page);
                var list = _rep.FindBy(findByPageAndDepartmentIdSpecification, out totalCount);
                _caching.Add(cachKey, new PagedData<Employee>(list, totalCount), 20);
                return list;
            }

            totalCount = pageData.TotalCount;
            return pageData.Data;
        }

        public IReadOnlyCollection<Employee> GetPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page, out int totalCount)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(pageSize, page, fromYear, toYear);

            if (fromYear > toYear) { throw new Exception("Неверно указаны параметры поиска"); }  // уточнить

            ISpecification FindPageBySearchParamsSpecification = _specificationCreator.CreateFindPageBySearchParamsSpecification(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, page);
            return _rep.FindBy(FindPageBySearchParamsSpecification, out totalCount);
        }
    }
}

