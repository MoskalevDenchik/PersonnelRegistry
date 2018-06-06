using DM.PR.Data.Core.InputParameters.Creaters;
using System.Collections.Generic;
using DM.PR.Data.Specifications;
using DM.PR.Common.Entities;
using System.Data.SqlClient;
using DM.PR.Data.Entity;
using System.Data;
using System;

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
                  new SqlParameter("@DepartmentId",item.Department.Id),
                  new SqlParameter("@LastName ",item.LastName),
                  new SqlParameter("@FirstName",item.FirstName),
                  new SqlParameter("@MiddleName",item.MiddleName),
                  new SqlParameter("@Address",item.Address),
                  new SqlParameter("@ImagePath",item.ImagePath),
                  new SqlParameter("@BeginningWork",item.BeginningWork),
                  new SqlParameter("@EndWork",item.EndWork),
                  new SqlParameter("@MaritalStatusId",item.MaritalStatus),
                  new SqlParameter("@Phones",ConvertToCreateTable(item.Phones)),
                  new SqlParameter("@Emails",ConvertToCreateTable(item.Emails))
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
                    new SqlParameter("@DepartmentId",item.Department.Id),
                    new SqlParameter("@LastName ",item.LastName),
                    new SqlParameter("@FirstName",item.FirstName),
                    new SqlParameter("@MiddleName",item.MiddleName),
                    new SqlParameter("@Address",item.Address),
                    new SqlParameter("@ImagePath",item.ImagePath),
                    new SqlParameter("@BeginningWork",item.BeginningWork),
                    new SqlParameter("@EndWork",item.EndWork),
                    new SqlParameter("@MaritalStatusId",item.MaritalStatus),
                    new SqlParameter("@Phones",ConvertToCreateTable(item.Phones)),
                    new SqlParameter("@Emails",ConvertToCreateTable(item.Emails))
                }
            };
        }

        public IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteEmployee",
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

        public IInputParameter CreateForFindPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, bool IsWorking, int pageSize, int page)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployeesBySearchParams",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@LastName", lastName),
                    new SqlParameter("@FirstName", firstName),
                    new SqlParameter("@MiddledName", middledName),
                    new SqlParameter("@FromYear", fromYear),
                    new SqlParameter("@ToYear", toYear),
                    new SqlParameter("@IsWorking", IsWorking),
                    new SqlParameter("@PageSize",pageSize),
                    new SqlParameter("@Page",page)
                }
            };
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
