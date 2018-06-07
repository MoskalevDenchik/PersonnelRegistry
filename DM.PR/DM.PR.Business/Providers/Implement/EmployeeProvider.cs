using DM.PR.Data.SpecificationCreators;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Providers.Implement
{
    internal class EmployeeProvider : IEmployeeProvider
    {
        private readonly IRepository<Employee> _rep;
        private readonly IEmployeeSpecificationCreator _creator;

        public EmployeeProvider(IRepository<Employee> rep, IEmployeeSpecificationCreator creator)
        {
            Helper.ThrowExceptionIfNull(rep, creator);
            _rep = rep;
            _creator = creator;
        }

        public Employee GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }

            return _rep.GetById(id);
        }

        public PagedData<Employee> GetPage(int pageSize, int page)
        {
            if (pageSize <= 0 || page <= 0)
            {
                throw new Exception("Id пришел 0");
            }

            ISpecification findByPageSpecification = _creator.CreateFindByPageDataSpecification(pageSize, page);
            return _rep.FindPageBy(findByPageSpecification);
        }

        public PagedData<Employee> GetPageByDepartmentId(int departmentId, int pageSize, int page)
        {
            if (departmentId <= 0 || pageSize <= 0 || page <= 0)
            {
                throw new Exception("Id пришел 0");
            }
            ISpecification findByPageAndDepartmentIdSpecification = _creator.CreateFindPageByDepartmentIdSpecification(departmentId, pageSize, page);
            return _rep.FindPageBy(findByPageAndDepartmentIdSpecification);
        }

        public PagedData<Employee> GetPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, bool IsWorking, int pageSize, int page)
        {
            if (pageSize <= 0 || page <= 0 || fromYear < 0 || toYear < 0 || fromYear > toYear)
            {
                throw new Exception("Id пришел  или параметрыры неверны");
            }
            ISpecification FindPageBySearchParamsSpecification = _creator.CreateFindPageBySearchParamsSpecification(lastName, firstName, middledName, fromYear, toYear, IsWorking, pageSize, page);
            return _rep.FindPageBy(FindPageBySearchParamsSpecification);
        }
    }
}

