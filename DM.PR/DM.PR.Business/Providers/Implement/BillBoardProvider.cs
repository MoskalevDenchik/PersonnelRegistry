using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System.Linq;

namespace DM.PR.Business.Providers.Implement
{
    internal class BillBoardProvider : Provider<BillBoard>
    {
        private readonly IRepository<BillBoard> _rep;
        private readonly IСacheStorage _cache;

        public BillBoardProvider(IRepository<BillBoard> rep, IСacheStorage cache) : base(rep)
        {
            Inspector.ThrowExceptionIfNull(rep, cache);
            _cache = cache;
            _rep = rep;
        }

        public override IReadOnlyCollection<BillBoard> GetAll()
        {
            var list = _cache.Get<IReadOnlyCollection<BillBoard>>("BillBoard");
            if (list == null)
            {
                list = _rep.GetAll();
                if (list != null)
                {
                    _cache.Add("BillBoard", list, 1);
                }
            }
            return list?.Take(2).ToList();
        }
    }
}
