
namespace DM.PR.Common.Entities
{
    public class EmployeeShortModel
    {
        public int Id { get; set; }

        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string WorkPhone { get; set; }
    }
}