using System.ComponentModel.DataAnnotations;
using DM.PR.WEB.Infrastructure.Attributes;
using System.Collections.Generic;
using DM.PR.Common.Attributes;
using System;

namespace DM.PR.Common.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public Department Department { get; set; }

        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 16 символов")]
        public string FirstName { get; set; }

        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 16 символов")]
        public string MiddleName { get; set; }

        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 16 символов")]
        public string LastName { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть до 16 символов")]
        public string MobilePhone { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть  до 16 символов")]
        public string HomePhone { get; set; }

        [StringLength(16, ErrorMessage = "Длина строки должна быть  до 16 символов")]
        public string WorkPhone { get; set; }

        [Emails]
        public List<Email> Emails { get; set; }

        [StringLength(64, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 64 символов")]
        public string Address { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public WorkStatus WorkStatus { get; set; }

        public string ImagePath { get; set; }

        public DateTime BeginningWork { get; set; }

        [ValidDate(OverOrEqualTo = "BeginningWork", ErrorMessage = "Дата увольнение не может быть раньше или совпадать с датой приема на работу")]
        public DateTime? EndWork { get; set; }

        public bool HasRole { get; set; }
    }
}