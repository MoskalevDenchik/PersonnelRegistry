using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.WEB.Models
{
    public class DepartmentSaveViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Отдел сверху")]
        public int ParentId { get; set; }

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
        public List<Phone> Phones { get; set; }

        public IReadOnlyCollection<DepartmentSelectModel> DepartmentList { get; set; }
    }
}