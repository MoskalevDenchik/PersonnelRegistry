using System.Collections.Generic;

namespace DM.PR.Data.Repositories
{
    public interface IAdRepository
    {
        IReadOnlyCollection<string> GetAll();
    }
}
