using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.MaritalStatus
{
    public class MaritalStatusCreateViewModel
    {
        [Required(ErrorMessage = "Введите статус")]
        public string Status { get; set; }
    }
}