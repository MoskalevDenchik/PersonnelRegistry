using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;                          

namespace DM.PR.Business.Services.Implement
{
    internal class KindPhoneService : IKindPhoneService
    {
        private readonly IRepository<KindPhone> _rep;

        public KindPhoneService(IRepository<KindPhone> rep)
        {
            Helper.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public void Create(KindPhone kindPhone)
        {
            _rep.Add(kindPhone);
        }

        public void Edit(KindPhone kindPhone)
        {
            _rep.Update(kindPhone);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }
            _rep.Remove(id);
        }
    }
}
