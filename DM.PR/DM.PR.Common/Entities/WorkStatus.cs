using System.ComponentModel.DataAnnotations;

namespace DM.PR.Common.Entities
{
    public class WorkStatus : IEntity
    {
        public int Id { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть до 16 символов")]
        public string Status { get; set; }
    }
}
