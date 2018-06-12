using DM.PR.Data.Core.DataBase.Data;
using DM.PR.Data.Core.Converters;
using System.Collections.Generic;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;
using System.Linq;

namespace DM.PR.Data.Core.Data.Implement
{
    internal class DbContext<T> : IDataContext<T>
    {
        private IConverter<T> _converter;
        private DbExec _dbExecutor;

        public DbContext(IConverter<T> converter, DbExec dbExecutor)
        {
            Inspector.ThrowExceptionIfNull(converter, dbExecutor);
            _converter = converter;
            _dbExecutor = dbExecutor;
        }

        public T GetEntity(IInputParameter parameter)
        {
            var executeResult = _dbExecutor.GetDataSet(parameter as DbInputParameter);
            return _converter.ConvertToList(executeResult).First();
        }

        public IReadOnlyCollection<T> GetEntities(IInputParameter parameter)
        {
            var executeResult = _dbExecutor.GetDataSet(parameter as DbInputParameter);
            return _converter.ConvertToList(executeResult).ToList();
        }

        public IReadOnlyCollection<T> GetEntities(IInputParameter parameter, out int outputParameter)
        {
            var executeResult = _dbExecutor.GetDataSet(parameter as DbInputParameter);
            return _converter.ConvertToList(executeResult, out outputParameter).ToList();
        }

        public void Save(IInputParameter parameter)
        {
            _dbExecutor.GetExecuteResult(parameter as DbInputParameter);
        }
    }
}
