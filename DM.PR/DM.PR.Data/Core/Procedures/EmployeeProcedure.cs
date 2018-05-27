namespace DM.PR.Data.Core.Procedures
{
    internal static class EmployeeProcedure
    {
        internal const string GetAll = "SelectAllEmployees";
        internal const string GetById = "SelectEmployeeById";
        internal const string GetAllByDepartmentId = "SelectEmployeesByDepartmentId";
        internal const string Create = "InsertEmployee";
        internal const string Update = "UpdateEmployee";
        internal const string Delete = "DeleteEmployee";
    }
}
