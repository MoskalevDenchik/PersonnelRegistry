using DM.PR.Common.Entities;          

namespace DM.PR.Business.Services
{
    public interface IKindPhoneService
    {
        void Create(KindPhone department);
        void Edit(KindPhone department);
        void Delete(int id);
    }
}
