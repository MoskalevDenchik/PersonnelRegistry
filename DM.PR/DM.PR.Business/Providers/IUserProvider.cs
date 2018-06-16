using DM.PR.Common.Entities.Account;

namespace DM.PR.Business.Providers
{
    public interface IUserProvider : IProvider<User>
    {
        User GetByLogin(string login);
        User GetByEmployeeId(int employeeId);
    }
}
