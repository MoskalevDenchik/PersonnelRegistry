using DM.PR.Common.Entities;        
using System.Collections.Generic;  

namespace DM.PR.Business.Providers
{
    public interface IKindPhoneProvider
    {
        IReadOnlyCollection<KindPhone> GetAll();
    }
}
