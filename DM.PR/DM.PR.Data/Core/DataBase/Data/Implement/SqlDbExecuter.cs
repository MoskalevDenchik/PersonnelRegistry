using System.Data.SqlClient;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;
using System.Data;
using System.Linq;

namespace DM.PR.Data.Core.DataBase.Data.Implement
{
    internal class SqlDbExecuter : DbExecuter
    {
        private readonly IConfigManger _configManager;

        public SqlDbExecuter(IConfigManger configManager)
        {
            Inspector.ThrowExceptionIfNull(configManager);
            _configManager = configManager;
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command) => new SqlDataAdapter(command as SqlCommand);

        protected override IDbConnection GetConnection() => new SqlConnection(_configManager.GetConnectionString("Connection"));

        protected override IDbCommand GetProcedureCommand(IDbConnection connection, DbInputParameter parameter)
        {
            var command = new SqlCommand(parameter.Procedure, connection as SqlConnection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            if (parameter.Parameters != null)
            {
                command.Parameters.AddRange(parameter.Parameters.Select(d => new SqlParameter($"@{d.Key}", d.Value)).ToArray());
            }
            return command;
        }
    }
}
