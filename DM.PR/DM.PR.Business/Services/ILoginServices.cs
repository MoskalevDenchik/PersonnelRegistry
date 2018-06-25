using DM.PR.Common.Entities;

namespace DM.PR.Business.Services
{
    public interface ILoginServices
    {
        Result SignIn(string login, string password);
        void SingOut();
    }
}
