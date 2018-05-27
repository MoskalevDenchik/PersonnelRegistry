using DM.PR.Common.Entities;
using System.Collections.Generic; 
namespace DM.PR.Data.Repositories
{
    public interface IKindPhoneRepository
    {
        IReadOnlyCollection<KindPhone> GetAll();
    }
}
