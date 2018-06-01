using System.Collections.Generic;
using System.Data;

namespace DM.PR.Data.Core.Converters
{
    internal interface IConverter<T>
    {
        IEnumerable<T> Convert(DataSet dataSet);
    }
}
