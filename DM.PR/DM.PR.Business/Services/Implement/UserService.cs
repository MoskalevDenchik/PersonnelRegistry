using DM.PR.Common.Entities.Account;
using DM.PR.Data.Repositories;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _rep;

        public UserService(IRepository<User> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public void Create(User user)
        {
            _rep.Add(user);
        }

        public void Edit(User user)
        {
            _rep.Update(user);
        }

        public void Delete(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            _rep.Remove(id);
        }
    }
}