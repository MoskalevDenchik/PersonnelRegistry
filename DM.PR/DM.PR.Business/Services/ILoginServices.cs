
using DM.PR.Common.Entities.Account;

namespace DM.PR.Business.Services
{
    public interface ILoginServices
    {
        SignInStatus SignIn(string login, string password);
        void SingOut();
    }
}
