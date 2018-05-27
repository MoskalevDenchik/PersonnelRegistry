using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.Core.Converters
{
    internal static class EmailConverter
    {
        internal static List<Email> Convert(SqlDataReader reader)
        {
            if (reader.NextResult() && reader.HasRows)
            {
                var mails = new List<Email>();

                while (reader.Read())
                {
                    mails.Add(new Email
                    {
                        Id = (int)reader["Id"],
                        Address = (string)reader["Adress"]
                    });
                }
                return mails;
            }
            else
            {
                return null;
            }
        }

        internal static List<Email> Convert(EnumerableRowCollection<DataRow> emails, string entityId, int id)
        {
            var listOfemails = from email in emails
                               where email.Field<int>(entityId) == id
                               select new
                               {
                                   Id = email.Field<int>("EmailId"),
                                   Address = email.Field<string>("Email")
                               };


            if (listOfemails != null)
            {
                var list = new List<Email>();
                foreach (var item in listOfemails)
                {
                    list.Add(new Email
                    {
                        Id = item.Id,
                        Address = item.Address
                    });
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
