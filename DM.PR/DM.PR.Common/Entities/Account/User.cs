using System.Collections.Generic;

namespace DM.PR.Common.Entities.Account
{
    public class User
    {
        public int Id { get; set; }          
        public string  Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
    }
}
