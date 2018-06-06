using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;    
using DM.PR.Common.Helpers;
using System.Data.Common;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.DataBase.Data.Implement
{
    internal class SqlDbExec : DbExec
    {
        IConfigManger _configManager;

        public SqlDbExec(IConfigManger configManager)
        {
            _configManager = configManager;
        }

        protected override DbDataAdapter GetAdapter(IDbCommand command)
        {
            IDbDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            return adapter as DbDataAdapter;
        }

        protected override IDbConnection GetConnection()
        {
            return new SqlConnection(_configManager.GetConnectionString("DataConnection"));
        }

        protected override IDbCommand GetProcedureCommand(DbInputParameter parameter)
        {
            var command = new SqlCommand
            {
                CommandText = parameter.Procedure,
                CommandType = CommandType.StoredProcedure
            };

            if (parameter.Parameters != null)
            {
                command.Parameters.AddRange(parameter.Parameters);
            }

            return command;
        }
    }
}
