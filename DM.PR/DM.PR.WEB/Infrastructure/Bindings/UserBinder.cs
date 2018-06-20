using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DM.PR.WEB.Infrastructure.Bindings
{
    public class UserBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            var user = new User
            {
                EmployeeId = (int)valueProvider.GetValue("EmployeeId").ConvertTo(typeof(int)),
                Login = (string)valueProvider.GetValue("Login").ConvertTo(typeof(string)),
                Password = (string)valueProvider.GetValue("Password").ConvertTo(typeof(string)),
                Roles = new List<Role>()
            };

            var roles = (int[])valueProvider.GetValue("Roles").ConvertTo(typeof(int[]));
            for (int i = 0; i < roles.Length; i++)
            {
                user.Roles.Add(new Role { Id = (int)roles[i] });
            }

            return user;
        }
    }
}