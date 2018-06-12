using System.Data.SqlClient;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;
using System.Data;

namespace DM.PR.Data.Core.DataBase.Data.Implement
{
    internal class SqlDbExec : DbExec
    {
        private readonly IConfigManger _configManager;

        public SqlDbExec(IConfigManger configManager)
        {
            Inspector.ThrowExceptionIfNull(configManager);
            _configManager = configManager;
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command)
        {
            return new SqlDataAdapter
            {
                SelectCommand = command as SqlCommand
            };
        }

        protected override IDbConnection GetConnection()
        {
            return new SqlConnection(_configManager.GetConnectionString("DataConnection"));
        }

        protected override IDbCommand GetProcedureCommand(IDbConnection connection, DbInputParameter parameter)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = parameter.Procedure,
                Connection = connection as SqlConnection,
                CommandType = CommandType.StoredProcedure,
            };

            if (parameter.Parameters != null)
            {
                command.Parameters.AddRange(parameter.Parameters);
            }

            return command;
        }
    }
}
