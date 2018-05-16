﻿using System;
using System.Collections.Generic;         

namespace DM.PR.Common.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public int? DepartmentId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public List<Phone> Phones { get; set; }

        public List<string> Emails { get; set; }

        public string Address { get; set; }

        public string MaritalStatus { get; set;}

        public string ImagePath { get; set; }

        public DateTime? BeginningOfWork { get; set; }

        public DateTime? EndOfWork { get; set; }
    }
}