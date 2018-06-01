using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.Procedures;
using DM.PR.Data.Entity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Repositories.Implement
{
    internal class KindPhoneRepository : IKindPhoneRepository
    {
        private readonly IDbExecutor _dBExecuter;
        private readonly IConverter<KindPhone> _converter;


        public KindPhoneRepository(IDbExecutor dBExecuter, IConverter<KindPhone> converter)
        {
            Helper.ThrowExceptionIfNull(dBExecuter, converter);
            _dBExecuter = dBExecuter;
            _converter = converter;
        }

        public IReadOnlyCollection<KindPhone> GetAll()
        {
            var executeResult = _dBExecuter.Execute(KindPhoneProcedure.GetAll, ResultType.DataSet);

            return _converter.Convert(executeResult.Result as DataSet).ToList();
        }
    }
}
