using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IMaritalStatusProvider
    {
        MaritalStatus GetById(int id);
        IReadOnlyCollection<MaritalStatus> GetAll();
    }
}
