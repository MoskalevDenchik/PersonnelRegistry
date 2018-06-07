using System.Collections.Generic;

namespace DM.PR.Common.Entities
{
    public class PagedData<T>
    {
        public IReadOnlyCollection<T> Data { get; set; }

        public int TotalCount { get; set; }


        public PagedData(IReadOnlyCollection<T> data, int totalCount)
        {
            data = Data;
            TotalCount = totalCount;
        }
    }
}