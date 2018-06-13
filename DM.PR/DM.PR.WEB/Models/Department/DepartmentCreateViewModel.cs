using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.WEB.Models
{
    public class DepartmentCreateViewModel
    {
        [Display(Name = "Отдел сверху")]
        public int? ParentId { get; set; }

        [Required(ErrorMessage = "Введите название отдела")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Телефоны")]
        public List<Phone> Phones { get; set; }
    }
}