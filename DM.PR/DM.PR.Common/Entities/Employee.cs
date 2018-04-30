﻿using DM.PR.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DM.PR.Common.Entities
{
    public class Employee
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Department Department { get; set; }

        public Phone Phones { get; set; }

        public string Emails { get; set; }

        public string Address { get; set; }

        public MaritalStatus MaritalStatus { get; set; }
        
    }
}