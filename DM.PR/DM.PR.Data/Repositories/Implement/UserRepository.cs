using DM.PR.Common.Entities.Account;
using DM.PR.Common.Helpers;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.Parameters;
using DM.PR.Data.Core.Procedures;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Repositories.Implement
{
    internal class UserRepository : IUserRepository
    {

        private readonly IDbExecutor _dbExecuter;
        private readonly IConverter<User> _converter;

        public UserRepository(IDbExecutor dbExecuter, IConverter<User> converter)
        {
            Helper.ThrowExceptionIfNull(dbExecuter, converter);
            _dbExecuter = dbExecuter;
            _converter = converter;
        }

        public User GetById(int id)
        {
            var executeResult = _dbExecuter.Execute(UserProcedure.GetById, Entity.ResultType.DataSet, UserParameters.ById(id));
            return _converter.Convert(executeResult.Result as DataSet).First();
        }

        public User GetByLogin(string login)
        {
            var executeResult = _dbExecuter.Execute(UserProcedure.GetByLogin, Entity.ResultType.DataSet, UserParameters.ByLogin(login));
            return _converter.Convert(executeResult.Result as DataSet).FirstOrDefault();
        }

        public IReadOnlyCollection<User> GetAll()
        {
            var executeResult = _dbExecuter.Execute(UserProcedure.GetAll);
            return _converter.Convert(executeResult.Result as DataSet).ToList();
        }
    }
}
