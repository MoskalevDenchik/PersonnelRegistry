using System.Collections.Generic;
using System.Data.SqlClient;
using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;
using System.Data;

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

        protected override IDbDataAdapter GetAdapter(IDbCommand command)
        {
            return new SqlDataAdapter
            {
                SelectCommand = command as SqlCommand
            };
        }

        protected override IDbConnection GetConnection()
        {
            return new SqlConnection(_configManager.GetConnectionString("Connection"));
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
                command.Parameters.AddRange(MapToSqlParameter(parameter.Parameters).ToArray());
            }

            return command;
        }

        private List<SqlParameter> MapToSqlParameter(Dictionary<string, object> parameters)
        {
            var dataParameters = new List<SqlParameter>();

            foreach (var item in parameters)
            {
                dataParameters.Add(new SqlParameter(item.Key, item.Value));
            }

            return dataParameters;
        }


    }
}
