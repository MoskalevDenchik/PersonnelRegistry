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

namespace DM.PR.Data.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        IDbExecutor _dbExecuter;

        public DepartmentRepository(IDbExecutor dbExecuter)
        {
            _dbExecuter = dbExecuter;

        }

        public Department GetById(int id)
        {
            var executeResult = _dbExecuter.Execute(DepartmentProcedure.GetById, parameters: DepartmentParameters.GetById(id));

            if (!executeResult.IsNull)
            {
                return DepartmentConverter.Convert(executeResult.Result as DataSet).First();
            }
            else
            {
                throw new Exception("Этого Id нет");
            }
        }

        public IReadOnlyCollection<Department> GetAll()
        {
            var executeResult = _dbExecuter.Execute(DepartmentProcedure.GetAll);

            if (!executeResult.IsNull)
            {
                return DepartmentConverter.Convert(executeResult.Result as DataSet).ToList();
            }
            else
            {
                return null;
            }
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
            return _dbExecuter.Execute(DepartmentProcedure.Delete, ResultType.NonQery, DepartmentParameters.Delete(id));
        }

    }





}

