using DM.PR.Common.Entities;       

namespace DM.PR.Business.Services
{
    interface IMaritalStatusService
    {
        void Create(MaritalStatus department);
        void Edit(MaritalStatus department);
        void Delete(int id);
    }
}
