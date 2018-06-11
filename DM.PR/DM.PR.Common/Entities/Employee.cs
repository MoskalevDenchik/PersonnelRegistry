using System.Collections.Generic;
using System;

namespace DM.PR.Common.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public Department Department { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public List<Phone> Phones { get; set; }

        public List<Email> Emails { get; set; }

        public string Address { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public WorkStatus WorkStatus { get; set; }

        public string ImagePath { get; set; }

        public DateTime? BeginningWork { get; set; }

        public DateTime? EndWork { get; set; }
    }
}