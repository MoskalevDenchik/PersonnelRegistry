using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DM.PR.WEB.Models
{
    public class DepartmentCreateViewModel
    {
        [Display(Name = "Отдел сверху")]
        public int? ParentId { get; set; }

        [Required(ErrorMessage = "Введите название отдела")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите адресс отдела")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите описание отдела")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Телефоны")]
        public List<string> Phones { get; set; }
    }
}