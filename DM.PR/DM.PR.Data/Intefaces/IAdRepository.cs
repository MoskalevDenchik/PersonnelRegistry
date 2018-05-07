using System.Collections.Generic;

namespace DM.PR.Data.Intefaces
{
    public interface IAdRepository
    {
        IEnumerable<string> GetAll();
    }
}
