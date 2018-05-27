using System;
using System.Collections.Generic;         

namespace DM.PR.Common.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public Department Department { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public List<Phone> Phones { get; set; }

        public List<Email> Emails { get; set; }

        public string Address { get; set; }

        public MaritalStatus MaritalStatus { get; set;}

        public string ImagePath { get; set; }

        public DateTime? BeginningWork { get; set; }

        public DateTime? EndWork { get; set; }
    }
}