using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.Core.Parameters
{
    internal static class DepartmentParameters
    {
        internal static SqlParameter GetById(int id)
        {
            return new SqlParameter("@Id", id);
        }

        internal static SqlParameter[] Create(Department department)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@Id", department.ParentId),
                new SqlParameter("@Name", department.Name),
                new SqlParameter("@Address", department.Address),
                new SqlParameter("@Description", department.Description),
                new SqlParameter("@Phones",ConvertToCreateTable(department.Phones))
            };
        }

        internal static SqlParameter[] Update(Department department)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@Id",department.Id),
                new SqlParameter("@ParentId", department.ParentId),
                new SqlParameter("@Name", department.Name),
                new SqlParameter("@Address", department.Address),
                new SqlParameter("@Description", department.Description),
                new SqlParameter("@Phones", ConvertToUpdateTable(department.Phones))

            };
        }

        internal static SqlParameter Delete(int id)
        {
            return new SqlParameter("@Id", id);
        }

        #region Helpers

        private static DataTable ConvertToCreateTable(IReadOnlyCollection<Phone> phones)
        {
            if (phones != null)
            {
                var table = CreatePhoneTable();

                foreach (var item in phones)
                {
                    table.Rows.Add(item.Number, item.Kind.Kind);
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
                var table = UpdatePhoneTable();

                foreach (var item in phones)
                {
                    table.Rows.Add(item.Id, item.Number, item.Kind.Kind);
                }
                return table;
            }
            else
            {
                return null;
            }
        }



        private static DataTable CreatePhoneTable()
        {
            var table = new DataTable("Phones");
            table.Columns.Add("Number", typeof(string));
            table.Columns.Add("KindId", typeof(int));

            return table;
        }

        private static DataTable UpdatePhoneTable()
        {
            var table = new DataTable("Phones");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Number", typeof(string));
            table.Columns.Add("KindId", typeof(int));

            return table;
        }

        #endregion
    }
}
