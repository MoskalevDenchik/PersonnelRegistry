using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.MaritalStatus
{
    public class MaritalStatusSaveViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите статус")]
        public string Status { get; set; }
    }
}