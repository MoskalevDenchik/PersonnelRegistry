﻿using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models.Employee
{
    public class EmployeeCreateViewModel
    {
        public int DepartmentId { get; set; }

        [Display(Name ="Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Emails")]
        public List<Email> Emails { get; set; }

        [Display(Name = "Телефоны")]
        public List<Phone> Phones { get; set; }

        [Display(Name = "Домашний адрес")]
        public string Address { get; set; }

        [Display(Name = "Семейный статус")]
        public string MaritalStatusId { get; set; }

        [Display(Name = "Фото")]
        public string ImagePath { get; set; }

        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.Date)]
        public DateTime? BeginningWork { get; set; }


        [DataType(DataType.Date)]
        public DateTime? EndWork { get; set; }
    }
}