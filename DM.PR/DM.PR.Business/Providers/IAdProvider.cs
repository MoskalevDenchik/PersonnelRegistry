using DM.PR.Common.Entities;
using System.Collections.Generic;   

namespace DM.PR.Business.Providers
{
    public interface IAdProvider
    {
        IReadOnlyCollection<BillBoard> GetContent();
    }
}
