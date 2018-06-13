using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.WorkStatus
{
    public class WorkStatusCreateViewModel
    {
        [Required(ErrorMessage = "Введите статус")]
        public string Status { get; set; }
    }
}