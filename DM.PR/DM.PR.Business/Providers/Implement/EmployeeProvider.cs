using DM.PR.Data.SpecificationCreators;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;
using DM.PR.Common.Services;

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
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<Employee> GetPage(int pageSize, int page, out int totalCount)
        {
            if (pageSize <= 0 || page <= 0)
            {
                throw new Exception("Id пришел 0");
            }
            var cachKey = $"Employees_{pageSize}_{page}";

            var pageData = _caching.Get<PagedData<Employee>>(cachKey);
            if (pageData == null)
            {
                ISpecification findByPageSpecification = _creator.CreateFindByPageDataSpecification(pageSize, page);
                var list = _rep.FindBy(findByPageSpecification, out totalCount);
                _caching.Add(cachKey, new PagedData<Employee>(list, totalCount), 20);
                return list;
            }

            totalCount = pageData.TotalCount;
            return pageData.Data;
        }

        public IReadOnlyCollection<Employee> GetPageByDepartmentId(int departmentId, int pageSize, int page, out int totalCount)
        {
            if (departmentId < 0 || pageSize <= 0 || page <= 0)
            {
                throw new Exception("Id пришел 0");
            }
            var cachKey = $"Employees_{pageSize}_{page}_{departmentId}";

            var pageData = _caching.Get<PagedData<Employee>>(cachKey);
            if (pageData == null)
            {
                ISpecification findByPageAndDepartmentIdSpecification = _creator.CreateFindPageByDepartmentIdSpecification(departmentId, pageSize, page);
                var list = _rep.FindBy(findByPageAndDepartmentIdSpecification, out totalCount);
                _caching.Add(cachKey, new PagedData<Employee>(list, totalCount), 20);
                return list;
            }

            totalCount = pageData.TotalCount;
            return pageData.Data;
        }

        public IReadOnlyCollection<Employee> GetPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page, out int totalCount)
        {
            if (pageSize <= 0 || page <= 0 || fromYear < 0 || toYear < 0 || fromYear > toYear)
            {
                throw new Exception("Id пришел  или параметрыры неверны");
            }
            ISpecification FindPageBySearchParamsSpecification = _creator.CreateFindPageBySearchParamsSpecification(lastName, firstName, middledName, fromYear, toYear, WorkStatusId, pageSize, page);
            return _rep.FindBy(FindPageBySearchParamsSpecification, out totalCount);
        }
    }
}

