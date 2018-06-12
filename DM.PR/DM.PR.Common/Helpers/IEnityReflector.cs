using System.Collections.Generic;

namespace DM.PR.Common.Helpers
{
    public interface IEnityReflector
    {
        List<object> GetPropertyValueList<T>(T obj);
    }
}
