using DM.PR.Common.Entities.Account;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IUserProvider
    {
        User GetById(int id);
        User GetByLogin(string login);
        User GetByEmployeeId(int employeeId);
        IReadOnlyCollection<User> GetAll();
    }
}
