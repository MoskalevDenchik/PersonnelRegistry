using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class KindPhoneConverter : IConverter<KindPhone>
    {
        public IEnumerable<KindPhone> Convert(DataSet dataSet)
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
