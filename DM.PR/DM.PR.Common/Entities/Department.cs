using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DM.PR.Common.Entities
{
    public class Department : IEntity
    {
        [Range(0, int.MaxValue, ErrorMessage = "Id не может быть отрицательным")]
        public int Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Id не может быть отрицательным")]
        public int ParentId { get; set; }

        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 16 символов")]
        public string Name { get; set; }

        [StringLength(64, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 64 символов")]
        public string Address { get; set; }

        [StringLength(128, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 128 символов")]
        public string Description { get; set; }

        public IReadOnlyCollection<Phone> Phones { get; set; }
    }
}
