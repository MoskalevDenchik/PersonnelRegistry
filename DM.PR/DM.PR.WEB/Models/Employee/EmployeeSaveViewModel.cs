using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System;

namespace DM.PR.WEB.Models.Employee
{
    public class EmployeeSaveViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Отдел")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        [Display(Name = "Emails")]
        public List<Email> Emails { get; set; }

        public string HomePhone { get; set; }

        [Required(ErrorMessage = "Введите рабочий телефон")]
        public string WorkPhone { get; set; }

        [Required(ErrorMessage = "Введите мобильный телефон")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Введите домашний адрес")]
        [Display(Name = "Домашний адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите семейное положение")]
        [Display(Name = "Семейный положение")]
        public int MaritalStatusId { get; set; }

        [Required(ErrorMessage = "Укажите статус")]
        [Display(Name = "Статус")]
        public int WorkStatusId { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Выберите дату приема на работу")]
        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.Date)]
        public DateTime BeginningWork { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndWork { get; set; }

        public IReadOnlyCollection<DepartmentSelectModel> DepartmentList { get; set; }

        public IReadOnlyCollection<Common.Entities.WorkStatus> WorkStatusList { get; set; }

        public IReadOnlyCollection<Common.Entities.MaritalStatus> MaritalStatusList { get; set; }
    }
}