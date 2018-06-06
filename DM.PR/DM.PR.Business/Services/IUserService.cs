using DM.PR.Common.Entities.Account;

namespace DM.PR.Business.Services
{
    public interface IUserService
    {
        void Create(User department);
        void Edit(User department);
        void Delete(int id);
    }
}
