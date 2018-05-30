using DM.PR.Common.Helpers;
using DM.PR.Data.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.Core.Data.Implement
{
    internal class DbExecutor : IDbExecutor
    {
        private readonly IDbAccess _access;

        private Func<SqlCommand, ExecuteResult> _funcResult;

        public DbExecutor(IDbAccess access)
        {
            Helper.ThrowExceptionIfNull(access);
            _access = access;
        }

        public ExecuteResult Execute(string procedure, ResultType result = ResultType.DataSet, params SqlParameter[] parameters)
        {
            switch (result)
            {
                case ResultType.DataSet:
                    _funcResult = GetDataSet;
                    break;
                case ResultType.DataTable:
                    _funcResult = GetDataTable;
                    break;
                case ResultType.Scalar:
                    _funcResult = GetScalar;
                    break;

                case ResultType.NonQery:
                    _funcResult = GetNonQeryResult;
                    break;
                default:
                    _funcResult = GetDataSet;
                    break;
            }

            return _access.ExecuteCommand(_funcResult, procedure, parameters);
        }

        private ExecuteResult GetDataSet(SqlCommand command)
        {
            using (var adapter = new SqlDataAdapter(command))
            {
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return new ExecuteResult(dataSet);
            }
        }
        private ExecuteResult GetDataTable(SqlCommand command)
        {
            using (var adapter = new SqlDataAdapter(command))
            {
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return new ExecuteResult(dataTable);
            }
        }
        private ExecuteResult GetNonQeryResult(SqlCommand command)
        {
            command.Connection.Open();
            var result = command.ExecuteNonQuery();
            return new ExecuteResult { Rows = result };
        }
        private ExecuteResult GetScalar(SqlCommand command)
        {
            command.Connection.Open();
            var result = command.ExecuteScalar();
            return new ExecuteResult(result);
        }
    }
}
