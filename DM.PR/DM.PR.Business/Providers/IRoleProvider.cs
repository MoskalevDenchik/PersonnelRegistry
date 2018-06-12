using DM.PR.Common.Entities.Account;
using System.Collections.Generic;    

namespace DM.PR.Business.Providers
{
    public interface IRoleProvider
    {
        Role GetById(int id);
        IReadOnlyCollection<Role> GetAll();
    }
}
