﻿using DM.PR.Common.Entities;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Parameters;
using DM.PR.Data.Core.Procedures;
using DM.PR.Data.Entity;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DM.PR.Data.Core.Data;
using DM.PR.Common.Helpers;

namespace DM.PR.Data.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbExecutor _dbExecuter;
        private readonly IConverter<Department> _converter;

        public DepartmentRepository(IDbExecutor dbExecuter, IConverter<Department> converter)
        {
            Helper.ThrowExceptionIfNull(dbExecuter, converter);
            _dbExecuter = dbExecuter;
            _converter = converter;
        }

        public Department GetById(int id)
        {
            var executeResult = _dbExecuter.Execute(DepartmentProcedure.GetById, ResultType.DataSet, DepartmentParameters.ById(id));
            return _converter.Convert(executeResult.Result as DataSet).First();
        }

        public IReadOnlyCollection<Department> GetAll()
        {
            var executeResult = _dbExecuter.Execute(DepartmentProcedure.GetAll, ResultType.DataSet);
            return _converter.Convert(executeResult.Result as DataSet).ToList();

        }

        public PagedData<Department> GetAll(int pageSize, int pageNumber)
        {
            var executeResult = _dbExecuter.Execute(DepartmentProcedure.GetAllByPage, ResultType.DataSet, DepartmentParameters.GetAll(pageSize, pageNumber));
            return new PagedData<Department>
            {
                Data = _converter.Convert(executeResult.Result as DataSet).ToList(),
                TotalCount = (executeResult.Result as DataSet).Tables[2].AsEnumerable().Select(x => x.Field<int>("Count")).First()
            };
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

