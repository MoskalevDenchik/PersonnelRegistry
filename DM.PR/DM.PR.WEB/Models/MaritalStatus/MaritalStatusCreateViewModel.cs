using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.MaritalStatus
{
    public class MaritalStatusCreateViewModel
    {
        [Required(ErrorMessage = "Введите семейное положение")]
        public string Status { get; set; }
    }
}