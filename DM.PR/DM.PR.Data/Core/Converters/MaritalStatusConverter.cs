using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Core.Converters
{
    internal static class MaritalStatusConverter
    {
        internal static IEnumerable<MaritalStatus> Convert(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(x =>
            {
                return new MaritalStatus
                {
                    Id = x.Field<int>("Id"),
                    Status = x.Field<string>("Status")
                };
            });
        }
    }
}
