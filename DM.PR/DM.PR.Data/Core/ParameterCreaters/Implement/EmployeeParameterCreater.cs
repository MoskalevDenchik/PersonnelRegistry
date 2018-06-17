using DM.PR.Data.Core.InputParameters.Creaters;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class EmployeeParameterCreater : IParameterCreater<Employee>, IEmployeeParameterCreater
    {
        public IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectEmployeeById",
                Parameters =
                {
                    {nameof(id), id}
                }
            };
        }

        public IInputParameter CreateGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllEmployees"
            };
        }

        public IInputParameter CreateSave(Employee item)
        {
            return new DbInputParameter
            {
                Procedure = "SaveEmployee",
                Parameters =
                {
                    {nameof(item.Id),item.Id},
                    {nameof(item.Department)+"Id",item.Department.Id},
                    {nameof(item.LastName),item.LastName},
                    {nameof(item.FirstName),item.FirstName},
                    {nameof(item.MiddleName),item.MiddleName},
                    {nameof(item.Address),item.Address},
                    {nameof(item.ImagePath),item.ImagePath},
                    {nameof(item.BeginningWork),item.BeginningWork},
                    {nameof(item.EndWork),item.EndWork},
                    {nameof(item.HomePhone),item.HomePhone},
                    {nameof(item.MobilePhone),item.MobilePhone},
                    {nameof(item.WorkPhone),item.WorkPhone},
                    {nameof(item.MaritalStatus)+"Id",item?.MaritalStatus?.Id??0},
                    {nameof(item.WorkStatus)+"Id",item?.WorkStatus?.Id??0},
                    {nameof(item.Emails),item.Emails!=null?ConvertToTable(item.Emails):null}
                }
            };
        }

        public IInputParameter CreateRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteEmployee",
                Parameters =
                {
                    {nameof(id), id }
                }
            };
        }

        public IInputParameter CreateFind(int pageSize, int pageNumber)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployees",
                Parameters =
                {
                    {nameof(pageSize), pageSize},
                    {nameof(pageNumber), pageNumber}
                }
            };
        }

        public IInputParameter CreateByDepartmentId(int departmentId, int pageSize, int pageNumber)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployeesByDepartmentId",
                Parameters =
                {
                    {nameof(departmentId), departmentId},
                    {nameof(pageSize), pageSize},
                    {nameof(pageNumber), pageNumber}
                }
            };
        }

        public IInputParameter CreateBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int pageNumber)
        {
            return new DbInputParameter
            {
                Procedure = "SelectPageEmployeesBySearchParams",
                Parameters =
                {
                   {nameof(lastName), lastName},
                   {nameof(firstName), firstName},
                   {nameof(middledName), middledName},
                   {nameof(fromYear), fromYear},
                   {nameof(toYear), toYear},
                   {nameof(WorkStatusId), WorkStatusId},
                   {nameof(pageSize),pageSize},
                   {nameof(pageNumber),pageNumber}
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


        #endregion
    }
}
