using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.PR.WEB.Models
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }

        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public List<Phone> Phones { get; set; }

        public List<Email> Emails { get; set; }

        public string Address { get; set; }

        public string MaritalStatus { get; set; }

        public string ImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BeginningOfWork { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndOfWork { get; set; }
    }
}