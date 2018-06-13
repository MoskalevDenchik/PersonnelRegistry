using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.KindPhone
{
    public class KindPhoneEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите тип связи")]
        public string Kind { get; set; }
    }
}