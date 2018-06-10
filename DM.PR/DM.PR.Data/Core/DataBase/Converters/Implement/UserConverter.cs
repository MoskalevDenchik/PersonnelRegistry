using DM.PR.Common.Entities.Account;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;
using DM.PR.Common.Entities;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class UserConverter : IConverter<User>
    {
        public IEnumerable<User> ConvertToList(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(x =>
            {
                return new User
                {
                    Id = x.Field<int>("Id"),
                    EmployeeId = x.Field<int>("EmployeeId"),
                    Login = x.Field<string>("Login"),
                    Password = x.Field<string>("Password"),
                    Emails = ConvertToEmails(x.Field<int>("Id"), dataSet.Tables[1]),
                    Roles = ConvertToRoles(x.Field<int>("Id"), dataSet.Tables[2])
                };
            });
        }

        public IEnumerable<User> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            throw new NotImplementedException();
        }

        private IReadOnlyCollection<Role> ConvertToRoles(int userId, DataTable table)
        {
            return table.AsEnumerable().Where(r => r.Field<int>("UserId") == userId).Select(r =>
            {
                return new Role
                {
                    Id = r.Field<int>("Id"),
                    Name = r.Field<string>("Name")
                };
            }).ToList();
        }

        private IReadOnlyCollection<Email> ConvertToEmails(int userId, DataTable table)
        {
            return table.AsEnumerable().Where(email => email.Field<int>("UserId") == userId).Select(email =>
            {
                return new Email
                {
                    Id = email.Field<int>("Id"),
                    Address = email.Field<string>("Address")
                };
            }).ToList();
        }
    }
}
