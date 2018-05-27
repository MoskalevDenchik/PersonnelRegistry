using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.Data;

namespace DM.PR.Data.Core.Converters
{
    internal static class KindPhoneConverter
    {
        public static IEnumerable<KindPhone> Convert(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(x =>
            {
                return new KindPhone
                {
                    Id = x.Field<int>("Id"),
                    Kind = x.Field<string>("Kind")
                };
            });
        }
    }
}
