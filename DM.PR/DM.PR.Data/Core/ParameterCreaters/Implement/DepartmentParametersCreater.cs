using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.InputParameters.Creaters.Implement
{
    internal class DepartmentParameterCreater : IParameterCreater<Department>, IDepartmentParameterCreater
    {
        public override IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectDepartmentById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllDepartments",
                Parameters = null
            };
        }

        public override IInputParameter CreateForAdd(Department item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertDepartment",
                Parameters =
                {
                    {"@Name", item.Name},
                    {"@Address", item.Address },
                    {"@ParentId", item.ParentId },
                    {"@Description", item.Description},
                    {"@Phones", item.Phones!=null? ConvertToTable(item.Phones):null}
                }
            };
        }

        public override IInputParameter CreateForUpdate(Department item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateDepartment",
                Parameters =
                {
                    {"@Id",item.Id},
                    {"@Name", item.Name },
                    {"@Address", item.Address},
                    {"@ParentId", item.ParentId},
                    {"@Description", item.Description},
                    {"@Phones", item.Phones!=null? ConvertToTable(item.Phones):null}
                }
            };
        }

        public override IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteDepartmentById",
                Parameters = { { "@Id", id } }
            };
        }

        public IInputParameter CreateForFindByPageData(int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllDepartmtsByPage",
                Parameters = { { "@PageSize", pageSize }, { "@Page", page } }
            };
        }

        #region Converters

        private static DataTable ConvertToTable(IReadOnlyCollection<Phone> phones)
        {
            var table = new DataTable("Phones");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Number", typeof(string));
            table.Columns.Add("KindId", typeof(int));

            foreach (var item in phones)
            {
                table.Rows.Add(item.Id, item.Number, item.Kind.Id);
            }
            return table;
        }

        #endregion
    }
}
