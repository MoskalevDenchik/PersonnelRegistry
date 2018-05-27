using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.Core.Parameters
{
    internal static class EmployeeParameters
    {
        internal static SqlParameter[] Create(Employee employee)
        {
            return new SqlParameter[]
               {
                  new SqlParameter("@DepartmentId",employee.Department.Id),
                  new SqlParameter("@LastName ",employee.LastName),
                  new SqlParameter("@FirstName",employee.FirstName),
                  new SqlParameter("@MiddleName",employee.MiddleName),
                  new SqlParameter("@Address",employee.Address),
                  new SqlParameter("@ImagePath",employee.ImagePath),
                  new SqlParameter("@BeginningWork",employee.BeginningWork),
                  new SqlParameter("@EndWork",employee.EndWork),
                  new SqlParameter("@MaritalStatusId",employee.MaritalStatus),
                  new SqlParameter("@Phones",ConvertToCreateTable(employee.Phones)),
                  new SqlParameter("@Emails",ConvertToCreateTable(employee.Emails))

            };
        }

        internal static SqlParameter[] Update(Employee employee)
        {
            return new SqlParameter[]
               {
                  new SqlParameter("@DepartmentId",employee.Department.Id),
                  new SqlParameter("@Id",employee.Id),
                  new SqlParameter("@LastName ",employee.LastName),
                  new SqlParameter("@FirstName",employee.FirstName),
                  new SqlParameter("@MiddleName",employee.MiddleName),
                  new SqlParameter("@Address",employee.Address),
                  new SqlParameter("@ImagePath",employee.ImagePath),
                  new SqlParameter("@BeginningOfWork",employee.BeginningWork),
                  new SqlParameter("@EndOfWork",employee.EndWork),
                  new SqlParameter("@MaritalStatusId",employee.MaritalStatus)
            };
        }

        internal static SqlParameter Delete(int id)
        {
            return new SqlParameter("@EmployeeId", id);
        }
        
        internal static SqlParameter ById(int id)
        {
            return new SqlParameter("@Id", id);
        }

        #region Helpers

        private static DataTable ConvertToCreateTable(IReadOnlyCollection<Email> emails)
        {
            if (emails != null)
            {
                var table = EmailTable();

                foreach (var item in emails)
                {
                    table.Rows.Add(item.Id, item.Address);
                }
                return table;

            }
            else
            {
                return null;
            }
        }

        private static DataTable ConvertToCreateTable(IReadOnlyCollection<Phone> phones)
        {
            if (phones != null)
            {
                var table = PhoneTable();

                foreach (var item in phones)
                {
                    table.Rows.Add(item.Id, item.Number, item.Kind);
                }
                return table;

            }
            else
            {
                return null;
            }
        }

        private static DataTable ConvertToUpdateTable(IReadOnlyCollection<Phone> phones)
        {
            if (phones != null)
            {
                var table = PhoneTable();

                foreach (var item in phones)
                {
                    table.Rows.Add(item.Id, item.Number, item.Kind);
                }
                return table;
            }
            else
            {
                return null;
            }
        }

        private static DataTable PhoneTable()
        {
            var table = new DataTable("Phones");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Number", typeof(string));
            table.Columns.Add("Type", typeof(int));

            return table;
        }

        private static DataTable EmailTable()
        {
            var table = new DataTable("Phones");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Address", typeof(string));

            return table;
        }
        #endregion
    }

}
