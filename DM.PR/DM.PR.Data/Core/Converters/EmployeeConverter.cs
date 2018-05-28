using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Core.Converters
{
    internal static class EmployeeConverter
    {
        public static IEnumerable<Employee> Convert(DataSet dataSet)
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
                    Department = dataSet.Tables[3].AsEnumerable().Where(dep => dep.Field<int>("Id") == empl.Field<int>("DepartmentId"))
                    .Select(dep =>
                    {
                        return new Department
                        {
                            Id = dep.Field<int>("Id"),
                            Name = dep.Field<string>("Name"),
                            ParentId = dep.Field<int?>("ParentId"),
                            Address = dep.Field<string>("Address"),
                            Description = dep.Field<string>("Description"),
                            Phones = dataSet.Tables[4].AsEnumerable().Where(phone => dep.Field<int>("Id") == phone.Field<int>("DepartmentId"))
                            .Select(phone =>
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
                            }).ToList()
                        };
                    }).FirstOrDefault(),
                    MaritalStatus = new MaritalStatus
                    {
                        Id = empl.Field<int>("StatusId"),
                        Status = empl.Field<string>("Status")
                    },
                    Emails = dataSet.Tables[2].AsEnumerable().Where(email => email.Field<int>("EmployeeId") == empl.Field<int>("Id"))
                    .Select(email =>
                    {
                        return new Email
                        {
                            Id = email.Field<int>("Id"),
                            Address = email.Field<string>("Address")
                        };
                    }).ToList(),
                    Phones = dataSet.Tables[1].AsEnumerable().Where(phone => phone.Field<int>("EmployeeId") == empl.Field<int>("Id"))
                    .Select(phone =>
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
                    }).ToList()
                };
            });
        }
    }
}
