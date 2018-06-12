using System.Collections.Generic;

namespace DM.PR.Common.Entities.Account
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IReadOnlyCollection<Email> Emails { get; set; }
        public IReadOnlyCollection<Role> Roles { get; set; }
    }
}
