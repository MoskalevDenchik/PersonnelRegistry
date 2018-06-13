using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.KindPhone
{
    public class KindPhoneCreateViewModel
    {
        [Required(ErrorMessage = "Введите тип связи")]
        public string Kind { get; set; }
    }
}