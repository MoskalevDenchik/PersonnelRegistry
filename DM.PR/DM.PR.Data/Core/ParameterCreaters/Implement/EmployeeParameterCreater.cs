using DM.PR.Data.Core.InputParameters.Creaters;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Common.Entities;
using System.Data.SqlClient;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class EmployeeParameterCreater : IParameterCreater<Employee>, IEmployeeParameterCreater
    {
        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectEmployeeById",
                Parameters = new SqlParameter[]
                {
                  new SqlParameter("@Id", id)
                }
            };
        }

        public IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllEmployees",
                Parameters = null
            };
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(Employee item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertEmployee",
                Parameters = new SqlParameter[]
                {
                  new SqlParameter("@DepartmentId",item?.Department.Id),
                  new SqlParameter("@LastName ",item.LastName),
                  new SqlParameter("@FirstName",item.FirstName),
                  new SqlParameter("@MiddleName",item.MiddleName),
                  new SqlParameter("@Address",item.Address),
                  new SqlParameter("@ImagePath",item.ImagePath),
                  new SqlParameter("@BeginningWork",item.BeginningWork),
                  new SqlParameter("@EndWork",item.EndWork),
                  new SqlParameter("@MaritalStatusId",item?.MaritalStatus.Id),
                  new SqlParameter("@WorkStatusId",item?.WorkStatus.Id),
                  new SqlParameter("@Phones",item.Phones!=null?ConvertToTable(item.Phones):null),
                  new SqlParameter("@Emails",item.Emails!=null?ConvertToTable(item.Emails):null)
                }
            };                                            
        }

        public IInputParameter CreateForUpdate(Employee item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateEmployee",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",item.Id),
                    new SqlParameter("@DepartmentId",item?.Department.Id),
                    new SqlParameter("@LastName ",item.LastName),
                    new SqlParameter("@FirstName",item.FirstName),
                    new SqlParameter("@MiddleName",item.MiddleName),
                    new SqlParameter("@Address",item.Address),
                    new SqlParameter("@ImagePath",item.ImagePath),
                    new SqlParameter("@BeginningWork",item.BeginningWork),
                    new SqlParameter("@EndWork",item.EndWork),
                    new SqlParameter("@WorkStatusId",item?.WorkStatus.Id),
                    new SqlParameter("@MaritalStatusId",item?.MaritalStatus.Id),
                    new SqlParameter("@Phones",item.Phones!=null?ConvertToTable(item.Phones):null),
                    new SqlParameter("@Emails",item.Emails!=null?ConvertToTable(item.Emails):null)
                }
            };
        }

        public IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteEmployeeById",
                Parameters = new SqlParameter[]
                {
                  new SqlParameter("@Id", id)
                }
            };
        }

        public IInputParameter CreateForFindByPageData(int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllEmployees",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",pageSize),
                    new SqlParameter("@Page",page)
                }
            };
        }

        public IInputParameter CreateForFindPageByDepartmentId(int departmentId, int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployeesByDepartmentId",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentId", departmentId),
                    new SqlParameter("@PageSize",pageSize),
                    new SqlParameter("@Page",page)
                }
            };
        }

        public IInputParameter CreateForFindPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployeesBySearchParams",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@LastName", lastName),
                    new SqlParameter("@FirstName", firstName),
                    new SqlParameter("@MiddleName", middledName),
                    new SqlParameter("@FromYear", fromYear),
                    new SqlParameter("@ToYear", toYear),
                    new SqlParameter("@WorkStatusId", WorkStatusId),
                    new SqlParameter("@PageSize",pageSize),
                    new SqlParameter("@Page",page)
                }
            };
        }

        #region Converters

        private static DataTable ConvertToTable(IReadOnlyCollection<Email> emails)
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
