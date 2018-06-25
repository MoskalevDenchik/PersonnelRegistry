using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Business.Providers;
using DM.PR.Business.Helpers;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Web.Security;
using System.Web;

namespace DM.PR.Business.Services.Implement
{
    internal class LoginServices : ILoginServices
    {
        #region Private

        private readonly IUserProvider _prov;

        #endregion

        #region Ctors
        public LoginServices(IUserProvider prov)
        {
            Inspector.ThrowExceptionIfNull(prov);
            _prov = prov;
        }

        #endregion

        public Result SignIn(string login, string password)
        {
            var result = new Result();

            var user = _prov.GetByLogin(login);
            if (user != null)
            {
                if (user.Password == password)
                {
                    AddUserToCookies(user);
                    result.Status = Status.Success;
                    result.Exceptions = null;
                }
                else
                {
                    result.Status = Status.InValid;
                    result.Exceptions = result.Exceptions = new List<ValidationResult> { new ValidationResult("Неверный пароль", new List<string> { "Password" }) };
                }
            }
            else
            {
                result.Status = Status.InValid;
                result.Exceptions = result.Exceptions = new List<ValidationResult> { new ValidationResult("Пользователя с таким именем не существует", new List<string> { "Login" }) };
            }

            return result;
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
