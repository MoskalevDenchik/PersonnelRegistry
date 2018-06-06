using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class DepartmentConverter : IConverter<Department>
    {
        public IEnumerable<Department> ConvertToList(DataSet dataSet)
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
                    Phones = ConvertToPhones(d.Field<int>("id"), dataSet.Tables[1])
                };
            });
        }

        public PagedData<Department> ConvertToPage(DataSet dataSet)
        {
            return new PagedData<Department>
            {
                Data = ConvertToList(dataSet).ToList(),
                TotalCount = dataSet.Tables[2].AsEnumerable().Select(x => x.Field<int>("Count")).First()
            };
        }


        private List<Phone> ConvertToPhones(int entityId, DataTable table)
        {
            return table.AsEnumerable().Where(phone => phone.Field<int>("DepartmentId") == entityId).Select(phone =>
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
    }
}
