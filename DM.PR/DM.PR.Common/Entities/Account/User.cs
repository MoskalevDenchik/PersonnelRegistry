using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Attributes;

namespace DM.PR.Common.Entities.Account
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [MaxLength(16, ErrorMessage = "Длинна строки должна быть не более 16 символов")]
        public string Login { get; set; }

        [MaxLength(16, ErrorMessage = "Длинна строки должна быть не более 16 символов")]
        public string Password { get; set; }

        public IReadOnlyCollection<Email> Emails { get; set; }

        [UniqRoles(ErrorMessage = "Роли не могу повторяться")]
        public List<Role> Roles { get; set; }
    }
}
