namespace DM.PR.WEB.Models.Employee
{
    public class EmployeeSummaryViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DepartmentName { get; set; }

        public string WorkPhone { get; set; }

        public string ImagePath { get; set; }

        public bool HasRole { get; set; }
    }
}