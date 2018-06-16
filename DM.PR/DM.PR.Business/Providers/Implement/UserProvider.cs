using DM.PR.Data.SpecificationCreators;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Specifications;
using DM.PR.Data.Repositories;
using DM.PR.Common.Helpers;
using System.Linq;

namespace DM.PR.Business.Providers.Implement
{
    internal class UserProvider : Provider<User>, IUserProvider
    {
        private readonly IRepository<User> _rep;
        private readonly IUserSpecificationCreator _specificCreator;

        public UserProvider(IRepository<User> rep, IUserSpecificationCreator specificCreator) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep, specificCreator);
            _specificCreator = specificCreator;
            _rep = rep;
        }

        public User GetByLogin(string login)
        {
            ISpecification specification = _specificCreator.CreateSpecification(login);
            return _rep.FindBy(specification).FirstOrDefault();
        }

        public User GetByEmployeeId(int employeeId)
        {
            ISpecification specification = _specificCreator.CreateSpecification(employeeId);
            return _rep.FindBy(specification).First();
        }
    }
}
