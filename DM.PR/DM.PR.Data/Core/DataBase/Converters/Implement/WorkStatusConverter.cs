using System.Collections.Generic;
using DM.PR.Data.Core.Converters;
using DM.PR.Common.Entities;
using System.Data;
using System;

namespace DM.PR.Data.Core.DataBase.Converters.Implement
{
    internal class WorkStatusConverter : IConverter<WorkStatus>
    {
        public IEnumerable<WorkStatus> ConvertToList(DataSet dataSet)
        {
            return dataSet.Tables[0].AsEnumerable().Select(x =>
            {
                return new WorkStatus
                {
                    Id = x.Field<int>("Id"),
                    Status = x.Field<string>("Status")
                };
            });
        }

        public IEnumerable<WorkStatus> ConvertToList(DataSet dataSet, out int outputParameter)
        {
            throw new NotImplementedException();
        }
    }
}
