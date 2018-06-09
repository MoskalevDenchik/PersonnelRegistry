using System.Collections.Generic;     
using DM.PR.Common.Entities;

namespace DM.PR.Business.Providers
{
    public interface IWorkStatusProvider
    {
        WorkStatus GetById(int id);
        IReadOnlyCollection<WorkStatus> GetAll();
    }
}
