using DM.PR.Common.Entities;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.Parameters;
using DM.PR.Data.Core.Procedures;
using DM.PR.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private IDbExecutor _dbExecutor;

        public EmployeeRepository(IDbExecutor dbExecutor)
        {
            _dbExecutor = dbExecutor;
        }

        public IReadOnlyCollection<Employee> GetAll()
        {
            var executeResult = _dbExecutor.Execute(EmployeeProcedure.GetAll);
            if (!executeResult.IsNull)
            {
                return EmployeeConverter.Convert(executeResult.Result as DataSet).ToList();
            }
            else
            {
                throw new Exception();
            }
        }

        public IReadOnlyCollection<Employee> GetAllByDepartmentId(int id)
        {
            var executeResult = _dbExecutor.Execute(EmployeeProcedure.GetAllByDepartmentId, ResultType.DataSet, EmployeeParameters.ById(id));
            if (!executeResult.IsNull)
            {
                return EmployeeConverter.Convert(executeResult.Result as DataSet).ToList();
            }
            else
            {
                throw new Exception();
            }
        }

        public Employee GetById(int id)
        {
            var executeResult = _dbExecutor.Execute(EmployeeProcedure.GetById, ResultType.DataSet, EmployeeParameters.ById(id));
            if (!executeResult.IsNull)
            {
                return EmployeeConverter.Convert(executeResult.Result as DataSet).First();
            }
            else
            {
                throw new Exception();
            }
        }

        public ExecuteResult Create(Employee employee)
        {
            return _dbExecutor.Execute(EmployeeProcedure.Create, ResultType.NonQery, EmployeeParameters.Create(employee));
        }

        public ExecuteResult Delete(int id)
        {
            return _dbExecutor.Execute(EmployeeProcedure.Delete, ResultType.NonQery, EmployeeParameters.Delete(id));
        }

        public ExecuteResult Update(Employee employee)
        {
            return _dbExecutor.Execute(EmployeeProcedure.Update, ResultType.NonQery, EmployeeParameters.Update(employee));
        }
    }
}

