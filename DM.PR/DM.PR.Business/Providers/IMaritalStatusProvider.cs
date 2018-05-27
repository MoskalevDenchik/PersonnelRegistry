using DM.PR.Common.Entities;
using System.Collections.Generic;

namespace DM.PR.Business.Providers
{
    public interface IMaritalStatusProvider
    {
        IReadOnlyCollection<MaritalStatus> GetAll();
    }
}
