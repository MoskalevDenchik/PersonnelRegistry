using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System;

namespace DM.PR.WEB.Models.Employee
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Выберите отдел")]
        [Display(Name ="Отдел")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name ="Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Emails")]
        public List<Email> Emails { get; set; }

        [Display(Name = "Домашний телефон")]
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "Введите рабочий телефон")]
        [Display(Name = "Рабочий телефон")]
        public string WorkPhone { get; set; }

        [Required(ErrorMessage = "Введите мобильный телефон")]
        [Display(Name = "Мобильный телефон")]
        public string MobilePhone { get; set; }

        [Display(Name = "Домашний адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите семейное положение")]
        [Display(Name = "Семейный положение")]
        public int MaritalStatusId { get; set; }

        [Required(ErrorMessage = "Укажите статус")]
        [Display(Name = "Статус")]
        public int  WorkStatusId { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Выберите дату приема на работу")]
        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.Date)]
        public DateTime BeginningWork { get; set; }

        [Display(Name = "Дата увольнения")]
        [DataType(DataType.Date)]
        public DateTime? EndWork { get; set; }
    }
}