using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Data;
using System.Linq;
using System;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class EmployeeConverter : IConverter<Employee>
    {
        public IEnumerable<Employee> ConvertToList(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(empl => new Employee
            {
                Id = empl.Field<int>("Id"),
                HasRole = empl.Field<bool>("HasRole"),
                Address = empl.Field<string>("Address"),
                LastName = empl.Field<string>("LastName"),
                EndWork = empl.Field<DateTime?>("EndWork"),
                ImagePath = empl.Field<string>("ImagePath"),
                FirstName = empl.Field<string>("FirstName"),
                HomePhone = empl.Field<string>("HomePhone"),
                WorkPhone = empl.Field<string>("WorkPhone"),
                MiddleName = empl.Field<string>("MiddleName"),
                MobilePhone = empl.Field<string>("MobilePhone"),
                BeginningWork = empl.Field<DateTime>("BeginningWork"),
                Emails = ConvertToEmails(empl.Field<int>("Id"), dataSet.Tables[1]),
                Department = ConvertToDepartmnent(empl.Field<int>("DepartmentId"), dataSet.Tables[2], dataSet.Tables[3]),
                WorkStatus = new WorkStatus { Id = empl.Field<int>("WorkStatusId"), Status = empl.Field<string>("WorkStatus") },
                MaritalStatus = new MaritalStatus { Id = empl.Field<int>("MaritalStatusId"), Status = empl.Field<string>("MaritalStatus") },
            });
        }

        public IEnumerable<Employee> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            outputParameter = dataSet.Tables[4].AsEnumerable().Select(x => x.Field<int>("Count")).First();
            return ConvertToList(dataSet);
        }

        #region Helpers

        private Department ConvertToDepartmnent(int departmentId, params DataTable[] tables)
        {
            return tables[0].AsEnumerable().Where(dep => dep.Field<int>("Id") == departmentId).Select(dep =>
            {
                return new Department
                {
                    Id = dep.Field<int>("Id"),
                    Name = dep.Field<string>("Name"),
                    ParentId = dep.Field<int?>("ParentId"),
                    Address = dep.Field<string>("Address"),
                    Description = dep.Field<string>("Description"),
                    Phones = tables[1].AsEnumerable().Where(phone => phone.Field<int>("DepartmentId") == departmentId).Select(phone => new Phone
                    {
                        Id = phone.Field<int>("Id"),
                        Number = phone.Field<string>("Number")
                    }).ToList()
                };
            }).FirstOrDefault();
        }
        private List<Email> ConvertToEmails(int employeeId, DataTable table)
        {
            return table.AsEnumerable().Where(email => email.Field<int>("EmployeeId") == employeeId).Select(email => new Email
            {
                Id = email.Field<int>("Id"),
                Address = email.Field<string>("Address")
            }).ToList();
        }

        #endregion
    }
}
