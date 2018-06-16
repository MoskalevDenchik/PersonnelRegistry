using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Linq;
using System.Data;
using System;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class UserConverter : IConverter<User>
    {
        public IEnumerable<User> ConvertToList(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(user => new User
            {
                Id = user.Field<int>("Id"),
                EmployeeId = user.Field<int>("EmployeeId"),
                Login = user.Field<string>("Login"),
                Password = user.Field<string>("Password"),
                Emails = ConvertToEmails(user.Field<int>("Id"), dataSet.Tables[1]),
                Roles = ConvertToRoles(user.Field<int>("Id"), dataSet.Tables[2])

            });
        }

        public IEnumerable<User> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            throw new NotImplementedException();
        }

        private IReadOnlyCollection<Role> ConvertToRoles(int userId, DataTable table)
        {
            return table.AsEnumerable().Where(r => r.Field<int>("UserId") == userId).Select(role => new Role
            {
                Id = role.Field<int>("Id"),
                Name = role.Field<string>("Name")
            }).ToList();
        }

        private IReadOnlyCollection<Email> ConvertToEmails(int userId, DataTable table)
        {
            return table.AsEnumerable().Where(email => email.Field<int>("UserId") == userId).Select(email => new Email
            {
                Id = email.Field<int>("Id"),
                Address = email.Field<string>("Address")
            }).ToList();
        }
    }
}
