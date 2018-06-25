using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.WorkStatus
{
    public class WorkStatusSaveViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите статус")]
        public string Status { get; set; }
    }
}