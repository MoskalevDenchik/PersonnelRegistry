using DM.PR.Common.Entities.Account;
using System.Collections.Generic;

namespace DM.PR.Data.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByLogin(string login);
        IReadOnlyCollection<User> GetAll();
    }
}
