using DM.PR.Common.Entities;

namespace DM.PR.Business.Services
{
    public interface IKindPhoneService
    {
        void Create(KindPhone kindPhone);
        void Edit(KindPhone kindPhone);
        void Delete(int id);
    }
}
