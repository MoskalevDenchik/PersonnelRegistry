using DM.PR.Common.Entities.Account;
using DM.PR.Common.Helpers;
using DM.PR.Data.Repositories;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Security;

namespace DM.PR.Business.Services.Implement
{
    internal class LoginServices : ILoginServices
    {
        IUserRepository _userRep;

        public LoginServices(IUserRepository userRep)
        {
            Helper.ThrowExceptionIfNull(userRep);
            _userRep = userRep;
        }

        #region Public Methods

        public SignInStatus SignIn(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return SignInStatus.Failure;
            }

            var user = _userRep.GetByLogin(login);

            if (user.Login.Equals(null))
            {
                return SignInStatus.Failure;
            }
            if (!user.Password.Equals(password))
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

        #endregion

        #region Helpers

        private void AddUserToCookies(User user)
        {
            var userData = JsonConvert.SerializeObject(user);
            var ticket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);
            var encryptTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        
        #endregion


    }
}
