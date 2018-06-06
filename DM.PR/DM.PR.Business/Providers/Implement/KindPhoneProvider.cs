using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Providers
{
    internal class KindPhoneProvider : IKindPhoneProvider
    {
        private readonly IRepository<KindPhone> _rep;

        public KindPhoneProvider(IRepository<KindPhone> rep)
        {
            Helper.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public KindPhone GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<KindPhone> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
