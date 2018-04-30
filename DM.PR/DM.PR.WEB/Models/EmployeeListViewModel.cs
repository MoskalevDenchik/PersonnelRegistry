using DM.PR.Common.Entities;

namespace DM.PR.WEB.Models
{
    public class EmployeeListViewModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DepartmentName { get; set; }

        public Phone WorkPhone { get; set; }
    }
}