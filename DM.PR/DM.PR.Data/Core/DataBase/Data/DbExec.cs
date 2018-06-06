using System.Data.Common;              
using DM.PR.Data.Entity;
using System.Data;
using System;

namespace DM.PR.Data.Core.DataBase.Data
{
    internal abstract class DbExec
    {
        protected abstract DbDataAdapter GetAdapter(IDbCommand command);

        protected abstract IDbConnection GetConnection();

        protected abstract IDbCommand GetProcedureCommand(DbInputParameter parameter);

        private T ExecuteCommand<T>(Func<IDbCommand, T> executeComand, DbInputParameter parameter)
        {
            using (IDbConnection connection = GetConnection())
            {
                using (IDbCommand command = GetProcedureCommand(parameter))
                {
                    command.Connection = connection;
                    return executeComand(command);
                }
            }
        }

        public DataSet GetDataSet(IInputParameter parameter)
        {
            return ExecuteCommand(command =>
            {
                using (DbDataAdapter adapter = GetAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
            , parameter as DbInputParameter);
        }

        public int GetNonQuery(IInputParameter parameter)
        {
            return ExecuteCommand(command =>
            {
                command.Connection.Open();
                int result = command.ExecuteNonQuery();
                return result;
            }
            , parameter as DbInputParameter);
        }
    }
}
