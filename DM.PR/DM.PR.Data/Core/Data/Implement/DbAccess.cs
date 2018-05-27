using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.Core.Data.Implement
{

    public class DbAccess : IDbAccess
    {
        IConfigManger _configManager;

        public DbAccess(IConfigManger configManager)
        {
            _configManager = configManager;
        }

        public T ExecuteCommand<T>(Func<SqlCommand,T> func, string procedure, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                using (var command = GetProcedureCommand(procedure, connection, parameters))
                {
                    return func(command);
                }
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configManager.GetConnectionString("DataConnection"));
        }

        private SqlCommand GetProcedureCommand(string procedure, SqlConnection connection, params SqlParameter[] parameters)
        {
            var command = new SqlCommand(procedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddRange(parameters);
            return command;
        }

    }

}

