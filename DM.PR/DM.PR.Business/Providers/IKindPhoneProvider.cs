using System.Collections.Generic;  
using DM.PR.Common.Entities;        

namespace DM.PR.Business.Providers
{
    public interface IKindPhoneProvider
    {
        KindPhone GetById(int id);
        IReadOnlyCollection<KindPhone> GetAll();
    }
}
