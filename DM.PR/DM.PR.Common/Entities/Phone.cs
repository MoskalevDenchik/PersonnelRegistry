using System.ComponentModel.DataAnnotations;

namespace DM.PR.Common.Entities
{
    public class Phone : IEntity
    {
        public int Id { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Number { get; set; }
    }
}
