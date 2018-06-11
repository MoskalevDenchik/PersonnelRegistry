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
        private readonly IСachingService _caching;
        private readonly IEmployeeSpecificationCreator _creator;

        public EmployeeProvider(IRepository<Employee> rep, IEmployeeSpecificationCreator creator, IСachingService caching)
        {
            Helper.ThrowExceptionIfNull(rep, creator, caching);
            _rep = rep;
            _creator = creator;
            _caching = caching;
        }

        public Employee GetById(int id)
        {
            Helper.ThrowExceptionIfZeroOrNegative(id);
            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Employee> GetPage(int pageSize, int page, out int totalCount)
        {
            Helper.ThrowExceptionIfZeroOrNegative(pageSize, page);

            var cachKey = $"Employees_{pageSize}_{page}";
            var pageData = _caching.Get<PagedData<Employee>>(cachKey);
            if (pageData == null)
            {
                var findByPageSpecification = _creator.CreateFindByPageDataSpecification(pageSize, page);
                var list = _rep.FindBy(findByPageSpecification, out totalCount);
                _caching.Add(cachKey, new PagedData<Employee>(list, totalCount), 20);
                return list;
            }

            totalCount = pageData.TotalCount;
            return pageData.Data;
        }

        public IReadOnlyCollection<Employee> GetPageByDepartmentId(int departmentId, int pageSize, int page, out int totalCount)
        {
            Helper.ThrowExceptionIfZeroOrNegative(departmentId, pageSize, page);

            var cachKey = $"Employees_{pageSize}_{page}_{departmentId}";
            var pageData = _caching.Get<PagedData<Employee>>(cachKey);
            if (pageData == null)
            {
                var findByPageAndDepartmentIdSpecification = _creator.CreateFindPageByDepartmentIdSpecification(departmentId, pageSize, page);
                var list = _rep.FindBy(findByPageAndDepartmentIdSpecification, out totalCount);
                _caching.Add(cachKey, new PagedData<Employee>(list, totalCount), 20);
                return list;
            }

            totalCount = pageData.TotalCount;
            return pageData.Data;
        }

        public IReadOnlyCollection<Employee> GetPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page, out int totalCount)
        {
            Helper.ThrowExceptionIfZeroOrNegative(pageSize, page, fromYear, toYear);

            if (fromYear > toYear) { throw new Exception("Id пришел  или параметрыры неверны"); }  // уточнить

            ISpecification FindPageBySearchParamsSpecification = _creator.CreateFindPageBySearchParamsSpecification(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, page);
            return _rep.FindBy(FindPageBySearchParamsSpecification, out totalCount);
        }
    }
}

