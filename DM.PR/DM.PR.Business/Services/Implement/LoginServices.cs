using DM.PR.Common.Entities.Account;
using DM.PR.Business.Providers;
using DM.PR.Business.Helpers;
using DM.PR.Common.Helpers;           
using System.Web.Security;
using System.Web;

namespace DM.PR.Business.Services.Implement
{
    internal class LoginServices : ILoginServices
    {
        private readonly IUserProvider _prov;

        public LoginServices(IUserProvider prov)
        {
            Inspector.ThrowExceptionIfNull(prov);
            _prov = prov;
        }

        public SignInStatus SignIn(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return SignInStatus.Failure;
            }

            var user = _prov.GetByLogin(login);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (user.Password != password)
            {
                return SignInStatus.InvalidPssword;
            }

            AddUserToCookies(user);
            return SignInStatus.Success;
        }


        public void SingOut()
        {
            FormsAuthentication.SignOut();
        }

        #region Helpers

        private void AddUserToCookies(User user)
        {
            var cookie = CookiesConverter.ConvertToCookie(user);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion


    }
}
