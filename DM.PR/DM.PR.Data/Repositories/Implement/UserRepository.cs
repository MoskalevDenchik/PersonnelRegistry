using DM.PR.Common.Entities.Account;
using DM.PR.Common.Helpers;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.Parameters;
using DM.PR.Data.Core.Procedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Repositories.Implement
{
    internal class UserRepository : IUserRepository
    {

        private readonly IDbExecutor _dbExecuter;

        public UserRepository(IDbExecutor dbExecuter)
        {
            Helper.ThrowExceptionIfNull(dbExecuter);
            _dbExecuter = dbExecuter;
        }

        public User GetById(int id)
        {
            var executeResult = _dbExecuter.Execute(UserProcedure.GetById, Entity.ResultType.DataSet, UserParameters.ById(id));
            return executeResult.IsNull ? throw new Exception() : UserConverter.Convert(executeResult.Result as DataSet).First();
        }

        public User GetByLogin(string login)
        {
            var executeResult = _dbExecuter.Execute(UserProcedure.GetByLogin, Entity.ResultType.DataSet, UserParameters.ByLogin(login));
            return executeResult.IsNull ? null : UserConverter.Convert(executeResult.Result as DataSet).FirstOrDefault();
        }

        public IReadOnlyCollection<User> GetAll()
        {
            var executeResult = _dbExecuter.Execute(UserProcedure.GetAll);
            return executeResult.IsNull ? null : UserConverter.Convert(executeResult.Result as DataSet).ToList();
        }
    }
}
