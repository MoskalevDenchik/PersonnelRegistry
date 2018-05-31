using DM.PR.Common.Entities.Account;
using System.Linq;
using System.Security.Principal;

namespace DM.PR.Business.Entities
{
    public class UserPrincipal : IPrincipal
    {
        public User User { get; set; }

        public IIdentity Identity { get; private set; }

        public UserPrincipal(User user)
        {
            User = user;
            Identity = new GenericIdentity(User.Login);
        }

        public bool IsInRole(string role)
        {
            return User.Roles.Select(r => r.Name).Contains(role);
        }
    }
}
