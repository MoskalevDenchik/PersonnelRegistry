using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System;

namespace DM.PR.WEB.Models
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Отдел")]
        public string DepartmentName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Домашний телефон")]
        public string HomePhone { get; set; }
                                                              
        [Display(Name = "Рабочий телефон")]
        public string WorkPhone { get; set; }
                                                              
        [Display(Name = "Мобильный телефон")]
        public string MobilePhone { get; set; }

        [Display(Name = "E-mails")]
        public List<Email> Emails { get; set; }

        [Display(Name = "Домашний адресс")]
        public string Address { get; set; }

        [Display(Name = "Семейное положение")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Статус")]
        public string WorkStatus { get; set; }
                                                      
        public string ImagePath { get; set; }

        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.Date)]
        public DateTime? BeginningOfWork { get; set; }

        [Display(Name = "Дата увольнения")]
        [DataType(DataType.Date)]
        public DateTime? EndOfWork { get; set; }
    }
}