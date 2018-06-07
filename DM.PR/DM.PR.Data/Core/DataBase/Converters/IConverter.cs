using System.Collections.Generic;
using System.Data;

namespace DM.PR.Data.Core.Converters
{
    internal interface IConverter<T>
    {
        IEnumerable<T> ConvertToList(DataSet dataSet);     
        IEnumerable<T> ConvertToList(DataSet dataSet, out int outputParameter);     
    }
}
