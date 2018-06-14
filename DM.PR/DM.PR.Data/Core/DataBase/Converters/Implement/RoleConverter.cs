using DM.PR.Common.Entities.Account;
using DM.PR.Data.Core.Converters;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;

namespace DM.PR.Data.Core.DataBase.Converters.Implement
{
    internal class RoleConverter : IConverter<Role>
    {
        public IEnumerable<Role> ConvertToList(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(x =>
            {
                return new Role
                {
                    Id = x.Field<int>("Id"),
                    Name = x.Field<string>("Name")
                };
            });
        }

        public IEnumerable<Role> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            throw new NotImplementedException();
        }
    }
}
