using System.Collections.Generic;   
using DM.PR.Common.Entities;

namespace DM.PR.Business.Providers
{
    public interface IBillBoardProvider
    {
        IReadOnlyCollection<BillBoard> GetAll();
    }
}
