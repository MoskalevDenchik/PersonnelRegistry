using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Data;
using System.Linq;
using System;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class KindPhoneConverter : IConverter<KindPhone>
    {
        public IEnumerable<KindPhone> ConvertToList(DataSet dataSet)
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

        public IEnumerable<KindPhone> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            throw new NotImplementedException();
        }
    }
}
