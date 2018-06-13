using System.ComponentModel.DataAnnotations;   
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System;

namespace DM.PR.WEB.Models.Employee
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Отдел")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Emails")]
        public List<Email> Emails { get; set; }

        [Display(Name = "Телефоны")]
        public List<Phone> Phones { get; set; }

        [Display(Name = "Домашний адрес")]
        public string Address { get; set; }

        [Display(Name = "Семейный положение")]
        public int MaritalStatusId { get; set; }

        [Display(Name = "Статус")]
        public int WorkStatusId { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.Date)]
        public DateTime BeginningWork { get; set; }

        [Display(Name = "Дата увольнения")]
        [DataType(DataType.Date)]
        public DateTime? EndWork { get; set; }
    }
}