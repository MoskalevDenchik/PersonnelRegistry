using DM.PR.Data.Core.InputParameters.Creaters;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class EmployeeParameterCreater : ParameterCreater<Employee>, IEmployeeParameterCreater
    {
        public override IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectEmployeeById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllEmployees",
                Parameters = null
            };
        }


        public override IInputParameter CreateAdd(Employee item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertEmployee",
                Parameters =
                {
                    {"@DepartmentId",item?.Department.Id},
                    {"@LastName ",item.LastName},
                    {"@FirstName",item.FirstName},
                    {"@MiddleName",item.MiddleName},
                    {"@Address",item.Address},
                    {"@ImagePath",item.ImagePath},
                    {"@BeginningWork",item.BeginningWork},
                    {"@EndWork",item.EndWork},
                    {"@MaritalStatusId",item?.MaritalStatus.Id},
                    {"@WorkStatusId",item?.WorkStatus.Id},
                    {"@Phones",item.Phones!=null?ConvertToTable(item.Phones):null},
                    {"@Emails",item.Emails!=null?ConvertToTable(item.Emails):null}
                }
            };
        }

        public override IInputParameter CreateUpdate(Employee item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateEmployee",
                Parameters =
                {
                    {"@Id",item.Id},
                    {"@DepartmentId",item?.Department.Id},
                    {"@LastName ",item.LastName},
                    {"@FirstName",item.FirstName},
                    {"@MiddleName",item.MiddleName},
                    {"@Address",item.Address},
                    {"@ImagePath",item.ImagePath},
                    {"@BeginningWork",item.BeginningWork},
                    {"@EndWork",item.EndWork},
                    {"@MaritalStatusId",item?.MaritalStatus.Id},
                    {"@WorkStatusId",item?.WorkStatus.Id},
                    {"@Phones",item.Phones!=null?ConvertToTable(item.Phones):null},
                    {"@Emails",item.Emails!=null?ConvertToTable(item.Emails):null}
                }
            };
        }

        public override IInputParameter CreateRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteEmployeeById",
                Parameters = { { "@Id", id } }
            };
        }

        public IInputParameter CreateFind(int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllEmployees",
                Parameters = { { "@PageSize", pageSize }, { "@Page", page } }
            };
        }

        public IInputParameter CreateByDepartmentId(int departmentId, int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployeesByDepartmentId",
                Parameters = { { "@DepartmentId", departmentId }, { "@PageSize", pageSize }, { "@Page", page } }
            };
        }

        public IInputParameter CreateBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployeesBySearchParams",
                Parameters =
                {
                   {"@LastName", lastName},
                   {"@FirstName", firstName},
                   {"@MiddleName", middledName},
                   {"@FromYear", fromYear},
                   {"@ToYear", toYear},
                   {"@WorkStatusId", WorkStatusId},
                   {"@PageSize",pageSize},
                   {"@Page",page}
                }
            };
        }

        #region Converters

        private DataTable ConvertToTable(IReadOnlyCollection<Email> emails)
        {
            var table = new DataTable("Emails");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Address", typeof(string));

            foreach (var item in emails)
            {
                table.Rows.Add(item.Id, item.Address);
            }
            return table;
        }

        private DataTable ConvertToTable(IReadOnlyCollection<Phone> phones)
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
