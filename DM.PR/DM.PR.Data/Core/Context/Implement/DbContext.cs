using System.Collections.Generic;
using DM.PR.Data.Core.Converters;
using DM.PR.Common.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;
using System.Linq;
using DM.PR.Data.Core.DataBase.Data;

namespace DM.PR.Data.Core.Data.Implement
{
    internal class DbContext<T> : IDataContext<T>
    {
        private IConverter<T> _converter;
        private DbExec _dbExecutor;

        public DbContext(IConverter<T> converter, DbExec dbExecutor)
        {
            Helper.ThrowExceptionIfNull(converter, dbExecutor);
            _converter = converter;
            _dbExecutor = dbExecutor;
        }

        public T GetEntity(IInputParameter parameter)
        {
            var executeResult = _dbExecutor.GetDataSet(parameter);
            return _converter.ConvertToList(executeResult).First();
        }

        public IReadOnlyCollection<T> GetEntities(IInputParameter parameter)
        {
            var executeResult = _dbExecutor.GetDataSet(parameter);
            return _converter.ConvertToList(executeResult).ToList();
        }

        public PagedData<T> GetPageEntities(IInputParameter parameter)
        {
            var executeResult = _dbExecutor.GetDataSet(parameter);
            return _converter.ConvertToPage(executeResult);
        }

        public void Save(IInputParameter parameter)
        {
            _dbExecutor.GetNonQuery(parameter);
        }
    }
}
