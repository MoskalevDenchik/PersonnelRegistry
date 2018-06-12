using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;

namespace DM.PR.Business.Providers.Implement
{
    internal class BillBoardProvider : IBillBoardProvider
    {
        private readonly IRepository<BillBoard> _rep;

        public BillBoardProvider(IRepository<BillBoard> rep)
        {
            Inspector.ThrowExceptionIfNull(rep);
            _rep = rep;
        }

        public IReadOnlyCollection<BillBoard> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
