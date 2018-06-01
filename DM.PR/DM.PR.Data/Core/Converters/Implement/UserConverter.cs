using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class UserConverter : IConverter<User>
    {
        public IEnumerable<User> Convert(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(x =>
            {
                return new User
                {
                    Id = x.Field<int>("Id"),
                    Login = x.Field<string>("Login"),
                    Email = x.Field<string>("Email"),
                    Password = x.Field<string>("Password"),
                    Roles = ConvertToRoles(dataSet.Tables[1])
                };
            });
        }

        private List<Role> ConvertToRoles(DataTable table)
        {
            return table.AsEnumerable().Select(r =>
            {
                return new Role
                {
                    Id = r.Field<int>("Id"),
                    Name = r.Field<string>("Name")
                };
            }).ToList();
        }
    }
}
