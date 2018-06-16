using System.ComponentModel.DataAnnotations;

namespace DM.PR.Common.Entities
{
    public class Email : IEntity
    {
        public int Id { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Address { get; set; }
    }
}