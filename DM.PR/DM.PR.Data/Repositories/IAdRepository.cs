using System.Collections.Generic;
using DM.PR.Common.Entities;

namespace DM.PR.Data.Repositories
{
    public interface IAdRepository
    {
        IReadOnlyCollection<BillBoard> GetAll();
    }
}
