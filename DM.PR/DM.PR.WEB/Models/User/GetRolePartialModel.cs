using DM.PR.Common.Entities.Account;
using System.Collections.Generic;

namespace DM.PR.WEB.Models.User
{
    public class GetRolePartialModel
    {
        public IReadOnlyCollection<Role> Roles { get; set; }
        public UserCreateViewModel Model { get; set; }
    }
}