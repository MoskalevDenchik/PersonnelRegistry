using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Services.Implement
{
    internal class WorkStatusService : IWorkStatusService
    {
        private readonly IRepository<WorkStatus> _rep;

        public WorkStatusService(IRepository<WorkStatus> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public void Create(WorkStatus workStatus)
        {
            _rep.Add(workStatus);
        }

        public void Edit(WorkStatus workStatus)
        {
            _rep.Update(workStatus);
        }

        public void Delete(int id)
        {
            Inspector.ThrowExceptionIfZeroOrNegative(id);
            _rep.Remove(id);
        }
    }
}
