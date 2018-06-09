using DM.PR.Common.Entities;    

namespace DM.PR.Business.Services
{
    public interface IWorkStatusService
    {
        void Create(WorkStatus workStatus);
        void Edit(WorkStatus workStatus);
        void Delete(int id);
    }
}
