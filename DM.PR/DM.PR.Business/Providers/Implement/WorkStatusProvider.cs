using System.Collections.Generic;
using DM.PR.Data.Repositories;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using System;

namespace DM.PR.Business.Providers.Implement
{
    internal class WorkStatusProvider : IWorkStatusProvider
    {
        private readonly IRepository<WorkStatus> _rep;

        public WorkStatusProvider(IRepository<WorkStatus> rep)
        {
            Helper.ThrowExceptionIfNull(rep);
            _rep = rep;
        }
        public WorkStatus GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Неверный ID");
            }

            return _rep.GetById(id);
        }

        public IReadOnlyCollection<WorkStatus> GetAll()
        {
            return _rep.GetAll();
        }
    }
}
