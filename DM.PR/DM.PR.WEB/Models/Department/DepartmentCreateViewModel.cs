using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models
{
    public class DepartmentCreateViewModel
    {
        [Display(Name = "Отдел сверху")]
        public int? ParentId { get; set; }

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