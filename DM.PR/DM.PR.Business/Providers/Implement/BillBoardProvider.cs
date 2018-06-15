using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers.Implement
{
    internal class BillBoardProvider : IBillBoardProvider
    {
        private readonly IRepository<BillBoard> _rep;
        private readonly IСacheStorage _cache;

        public BillBoardProvider(IRepository<BillBoard> rep, IСacheStorage cache)
        {
            Inspector.ThrowExceptionIfNull(rep, cache);
            _cache = cache;
            _rep = rep;
        }

        public IReadOnlyCollection<BillBoard> GetAll()
        {
            var list = _cache.Get<IReadOnlyCollection<BillBoard>>("BillBoard");
            if (list == null)
            {
                var data = _rep.GetAll();
                _cache.Add("BillBoard", data, 1);
                return data;
            }
            return list;
        }
    }
}
