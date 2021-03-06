﻿using System.Data.Common;
using DM.PR.Data.Entity;
using System.Data;
using System;

namespace DM.PR.Data.Core.DataBase.Data
{
    internal abstract class DbExecuter
    {
        protected abstract IDbConnection GetConnection();
        protected abstract IDbDataAdapter GetAdapter(IDbCommand command);
        protected abstract IDbCommand GetProcedureCommand(IDbConnection connection, DbInputParameter parameter);

        private T ExecuteCommand<T>(Func<IDbCommand, T> executeCommand, DbInputParameter parameter)
        {
            using (IDbConnection connection = GetConnection())
            using (IDbCommand command = GetProcedureCommand(connection, parameter))
            {
                return executeCommand(command);
            }
        }

        public DataSet GetDataSet(DbInputParameter parameter) => ExecuteCommand(command =>
        {
            using (var adapter = GetAdapter(command) as DbDataAdapter)
            {
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }, parameter);


        public int GetExecuteResult(DbInputParameter parameter) => ExecuteCommand(command =>
        {
            command.Connection.Open();
            return command.ExecuteNonQuery();

        }, parameter);
    }
}
