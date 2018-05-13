namespace DM.PR.Data.DataBase
{
    public static class Procedure
    {
        #region Employees

        public const string GetEmployeeById = "GetEmployeeById";
        public const string GetAllEmployees = "GetAllEmployees";
        public const string AddEmployee = "AddEmployee";
        public const string UpdateEmployee = "UpdateEmployee";
        public const string GetEmployeesByDepartmentId = "GetEmployeesByDepartmentId";
        public const string DeleteEmployee = "DeleteEmployee";
        public const string GetShortEmploees = "GetAllSortEmployees";
        public const string GetAllShortModelsByDepartmentId = "GetAllShortByDepartmentId";

        #endregion

        #region Departments

        public const string GetDepartmentById = "GetDepartmentById";
        public const string GetAllDepartmts = "GetAllDepartmts";
        public const string GetAllDepartmentNav = "GetAllDepartmentNav";
        public const string AddDepartment = "AddDepartment";
        public const string DeleteDepartment = "DeleteDepartmentById";
        public const string UpdateDepartment = "UpdateDepartment";
        public const string AddPhoneByDepartmentId = "AddPhoneByDepartmentId";


        #endregion
    }

}