using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace DM.PR.Common.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public Department Department { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть до 16 символов")]
        public string FirstName { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть до 16 символов")]
        public string MiddleName { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть до 16 символов")]
        public string LastName { get; set; }

        public string MobilePhone { get; set; }

        public string HomePhone { get; set; }

        public string WorkPhone { get; set; }

        public List<Email> Emails { get; set; }

        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Address { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public WorkStatus WorkStatus { get; set; }

        public string ImagePath { get; set; }

        public DateTime BeginningWork { get; set; }

        public DateTime? EndWork { get; set; }

        public bool HasRole { get; set; }
    }
}