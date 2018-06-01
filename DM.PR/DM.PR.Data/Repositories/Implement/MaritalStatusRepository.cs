using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.Procedures;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Repositories.Implement
{
    internal class MaritalStatusRepository : IMaritalStatusRepository
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IConverter<MaritalStatus> _converter;

        public MaritalStatusRepository(IDbExecutor dbExecutor, IConverter<MaritalStatus> converter)
        {
            Helper.ThrowExceptionIfNull(dbExecutor, converter);
            _dbExecutor = dbExecutor;
            _converter = converter;
        }

        public IReadOnlyCollection<MaritalStatus> GetAll()
        {
            var result = _dbExecutor.Execute(MaritalStatusProcedure.GetAll);

            return _converter.Convert(result.Result as DataSet).ToList();
        }
    }
}
