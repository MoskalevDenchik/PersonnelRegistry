using DM.PR.Common.Entities.Account;
using System.Collections.Generic; 

namespace DM.PR.WEB.Models.User
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
    }
}