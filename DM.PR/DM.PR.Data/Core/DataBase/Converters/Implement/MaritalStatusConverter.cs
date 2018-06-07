using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Data;
using System.Linq;
using System;

namespace DM.PR.Data.Core.Converters.Implement
{
    internal class MaritalStatusConverter : IConverter<MaritalStatus>
    {
        public IEnumerable<MaritalStatus> ConvertToList(DataSet dataSet)
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

        public IEnumerable<MaritalStatus> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            throw new NotImplementedException();
        }
       
    }
}