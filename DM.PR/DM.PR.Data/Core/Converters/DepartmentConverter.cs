using DM.PR.Common.Entities;   
using System.Collections.Generic;
using System.Data;            
using System.Linq;

namespace DM.PR.Data.Core.Converters
{
    internal static class DepartmentConverter
    {   
        internal static IEnumerable<Department> Convert(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(d =>
            {
                return new Department
                {
                    Id = d.Field<int>("id"),
                    ParentId = d.Field<int?>("ParentId"),
                    Name = d.Field<string>("Name"),
                    Address = d.Field<string>("Address"),
                    Description = d.Field<string>("Description"),
                    Phones = dataSet.Tables[1].AsEnumerable()
                    .Where(p => p.Field<int>("DepartmentId") == d.Field<int>("id")).Select(p =>
                    {
                        return new Phone
                        {
                            Id = p.Field<int>("id"),
                            Number = p.Field<string>("Number"),
                            Kind = new KindPhone
                            {
                                Id = p.Field<int>("KindId"),
                                Kind = p.Field<string>("Kind")
                            }
                        };
                    }).ToList()
                };
            });
        }             
    }
}

