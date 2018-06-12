using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class MaritalStatusService : IMaritalStatusService
    {
        private readonly IRepository<MaritalStatus> _rep;

        public MaritalStatusService(IRepository<MaritalStatus> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public void Create(MaritalStatus maritalStatus)
        {
            _rep.Add(maritalStatus);
        }

        public void Edit(MaritalStatus maritalStatus)
        {
            _rep.Update(maritalStatus);
        }

        public void Delete(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            _rep.Remove(id);
        }
    }
}
