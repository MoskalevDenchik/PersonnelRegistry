using DM.PR.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.PR.Common.Entities
{
    public class Employee
    {
        public int? Id { get; set; }
        public Department Department { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<Phone> Phones { get; set; }
        public List<string> Emails { get; set; }
        public string Address { get; set; }
        public MaritalStatus MaritalStatus { get; set;}

        [DataType(DataType.Date)]
        public DateTime? BeginningOfWork { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndOfWork { get; set; }
    }
}