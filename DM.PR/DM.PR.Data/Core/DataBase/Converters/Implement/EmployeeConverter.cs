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
            return dataSet.Tables[0].AsEnumerable().Select(empl =>
            {
                return new Employee
                {
                    Id = empl.Field<int>("Id"),
                    FirstName = empl.Field<string>("FirstName"),
                    LastName = empl.Field<string>("LastName"),
                    MiddleName = empl.Field<string>("MiddleName"),
                    Address = empl.Field<string>("Address"),
                    BeginningWork = empl.Field<DateTime?>("BeginningWork"),
                    EndWork = empl.Field<DateTime?>("EndWork"),
                    ImagePath = empl.Field<string>("ImagePath"),
                    Phones = ConvertToPhones(empl.Field<int>("Id"), "EmployeeId", dataSet.Tables[1]),
                    Emails = ConvertToEmails(empl.Field<int>("Id"), dataSet.Tables[2]),
                    Department = ConvertToDepartmnent(empl.Field<int>("DepartmentId"), dataSet.Tables[3], dataSet.Tables[4]),
                    MaritalStatus = new MaritalStatus
                    {
                        Id = empl.Field<int>("MaritalStatusId"),
                        Status = empl.Field<string>("MaritalStatus")
                    },
                    WorkStatus = new WorkStatus
                    {
                        Id = empl.Field<int>("WorkStatusId"),
                        Status = empl.Field<string>("WorkStatus")
                    }
                };
            });
        }

        public IEnumerable<Employee> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            outputParameter = dataSet.Tables[5].AsEnumerable().Select(x => x.Field<int>("Count")).First();
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
                    Phones = ConvertToPhones(dep.Field<int>("Id"), "DepartmentId", tables[1])
                };
            }).FirstOrDefault();
        }
        private List<Email> ConvertToEmails(int employeeId, DataTable table)
        {
            return table.AsEnumerable().Where(email => email.Field<int>("EmployeeId") == employeeId).Select(email =>
            {
                return new Email
                {
                    Id = email.Field<int>("Id"),
                    Address = email.Field<string>("Address")
                };
            }).ToList();
        }
        private List<Phone> ConvertToPhones(int entityId, string EntityName, DataTable table)
        {
            return table.AsEnumerable().Where(phone => phone.Field<int>(EntityName) == entityId).Select(phone =>
            {
                return new Phone
                {
                    Id = phone.Field<int>("Id"),
                    Number = phone.Field<string>("Number"),
                    Kind = new KindPhone
                    {
                        Id = phone.Field<int>("KindId"),
                        Kind = phone.Field<string>("Kind")
                    }
                };
            }).ToList();
        }

        #endregion
    }
}
