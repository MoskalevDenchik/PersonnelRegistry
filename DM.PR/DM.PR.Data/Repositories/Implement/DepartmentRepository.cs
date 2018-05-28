using DM.PR.Common.Entities;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Parameters;
using DM.PR.Data.Core.Procedures;
using DM.PR.Data.Entity;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using DM.PR.Data.Core.Data;
using DM.PR.Common.Helpers;

namespace DM.PR.Data.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbExecutor _dbExecuter;

        public DepartmentRepository(IDbExecutor dbExecuter)
        {
            Helper.ThrowExceptionIfNull(dbExecuter);
            _dbExecuter = dbExecuter;
        }

        public Department GetById(int id)
        {
            var executeResult = _dbExecuter.Execute(DepartmentProcedure.GetById, ResultType.DataSet, DepartmentParameters.ById(id));

            return executeResult.IsNull ? throw new Exception() : DepartmentConverter.Convert(executeResult.Result as DataSet).First();
        }

        public IReadOnlyCollection<Department> GetAll()
        {
            var executeResult = _dbExecuter.Execute(DepartmentProcedure.GetAll);

            return executeResult.IsNull ? null : DepartmentConverter.Convert(executeResult.Result as DataSet).ToList();
        }

        public ExecuteResult Create(Department department)
        {
            return _dbExecuter.Execute(DepartmentProcedure.Create, ResultType.NonQery, DepartmentParameters.Create(department));
        }

        public ExecuteResult Update(Department department)
        {
            return _dbExecuter.Execute(DepartmentProcedure.Update, ResultType.NonQery, DepartmentParameters.Update(department));

        }
        public ExecuteResult Delete(int id)
        {
            return _dbExecuter.Execute(DepartmentProcedure.Delete, ResultType.NonQery, DepartmentParameters.ById(id));
        }
    }   
}

