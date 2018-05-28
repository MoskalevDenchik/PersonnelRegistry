using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.Procedures;
using DM.PR.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Repositories.Implement
{
    internal class KindPhoneRepository : IKindPhoneRepository
    {
        private readonly IDbExecutor _dBExecuter;

        public KindPhoneRepository(IDbExecutor dBExecuter)
        {
            Helper.ThrowExceptionIfNull(dBExecuter);
            _dBExecuter = dBExecuter;
        }

        public IReadOnlyCollection<KindPhone> GetAll()
        {
            var executeResult = _dBExecuter.Execute(KindPhoneProcedure.GetAll, ResultType.DataSet);

            return executeResult.IsNull ? throw new Exception() : KindPhoneConverter.Convert(executeResult.Result as DataSet).ToList();
        }
    }
}
