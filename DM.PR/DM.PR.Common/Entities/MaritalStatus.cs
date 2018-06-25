using System.ComponentModel.DataAnnotations;

namespace DM.PR.Common.Entities
{
    public class MaritalStatus : IEntity
    {
        public int Id { get; set; }

        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 16 символов")]
        public string Status { get; set; }
    }
}
