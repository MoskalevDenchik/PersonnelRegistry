using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.MaritalStatus
{
    public class MaritalStatusEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите семейное положение")]
        public string Status { get; set; }
    }
}