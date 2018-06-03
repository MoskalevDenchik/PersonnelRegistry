namespace DM.PR.Data.Core.Procedures
{
    internal static class EmployeeProcedure
    {
        public const string GetAll = "SelectAllEmployees";
        public const string GetById = "SelectEmployeeById";
        public const string GetAllByDepartmentId = "SelectEmployeesByDepartmentId";
        public const string Create = "InsertEmployee";
        public const string Update = "UpdateEmployee";
        public const string Delete = "DeleteEmployee";
        public const string FindBy = "FindEmpoyeesBy";
    }
}
