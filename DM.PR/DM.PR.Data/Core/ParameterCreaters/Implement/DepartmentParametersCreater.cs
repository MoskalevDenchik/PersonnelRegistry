using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Common.Entities;
using System.Data.SqlClient;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.InputParameters.Creaters.Implement
{
    internal class DepartmentParameterCreater : IParameterCreater<Department>, IDepartmentParameterCreater
    {
        public IInputParameter CreateForAdd(Department item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertDepartment",

                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", item.ParentId),
                    new SqlParameter("@Name", item.Name),
                    new SqlParameter("@Address", item.Address),
                    new SqlParameter("@Description", item.Description),
                    new SqlParameter("@Phones",ConvertToCreateTable(item.Phones))
                }
            };
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllDepartments",
                Parameters = null
            };
        }

        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectDepartmentById",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                }
            };
        }

        public IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteDepartmentById",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                }
            };
        }

        public IInputParameter CreateForUpdate(Department item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateDepartment",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",item.Id),
                    new SqlParameter("@ParentId", item.ParentId),
                    new SqlParameter("@Name", item.Name),
                    new SqlParameter("@Address", item.Address),
                    new SqlParameter("@Description", item.Description),
                    new SqlParameter("@Phones", ConvertToUpdateTable(item.Phones))

                }
            };
        }

        public IInputParameter CreateForFindByPageData(int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllDepartmtsByPage",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",pageSize),
                    new SqlParameter("@Page",page)
                }
            };
        }
        

        #region Helpers

        private static DataTable ConvertToCreateTable(IReadOnlyCollection<Phone> phones)
        {
            if (phones == null)
            {
                return null;
            }
            var table = CreatePhoneTable();

            foreach (var item in phones)
            {
                table.Rows.Add(item.Number, item.Kind.Kind);
            }
            return table;
        }


        private static DataTable ConvertToUpdateTable(IReadOnlyCollection<Phone> phones)
        {
            if (phones == null)
            {
                return null;
            }

            var table = UpdatePhoneTable();

            foreach (var item in phones)
            {
                table.Rows.Add(item.Id, item.Number, item.Kind.Kind);
            }
            return table;
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
