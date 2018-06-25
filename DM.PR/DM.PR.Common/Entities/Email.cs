using System.ComponentModel.DataAnnotations;

namespace DM.PR.Common.Entities
{
    public class Email : IEntity
    {
        public int Id { get; set; }

        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 16 символов")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Неверно введен E-mail")]
        public string Address { get; set; }
    }
}