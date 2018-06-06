using DM.PR.Common.Entities;
using System.Collections.Generic;
using System.Data;

namespace DM.PR.Data.Core.Converters
{
    internal interface IConverter<T>
    {
        IEnumerable<T> ConvertToList(DataSet dataSet);
        PagedData<T> ConvertToPage(DataSet dataSet);
    }
}
