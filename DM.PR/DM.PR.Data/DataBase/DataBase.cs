using DM.PR.Common.Logger;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.DataBase

{
    public class DataBase : IDataBase
    {
        #region Private

        private IRecordLog _log;
        private readonly string _conStr;
        private static SqlConnection _connection;

        #endregion

        #region Ctor

        public DataBase(IRecordLog log)
        {
            _log = log;
            try
            {
                _conStr = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
            }
            catch (Exception ex )
            {
                _log.MakeInfo(ex.Message);
                _conStr = null;
            }
           
        }

        #endregion

        #region ExecuteReader

        public SqlDataReader GetReader(string procedure, string parametrName, int? Id)
        {
            SqlDataReader reader;

            var command = GetCommandProcedure(procedure);

            command.Parameters.AddWithValue(parametrName, Id);

            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                reader = null;
            }

            if (reader != null && reader.HasRows)
            {
                return reader;
            }
            else
            {
                CloseConnection();
                return null;
            }
        }

        public SqlDataReader GetReader(string procedure, params SqlParameter[] parameters)
        {
            SqlDataReader reader;

            var command = GetCommandProcedure(procedure);

            AddParameters(command, parameters);

            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                reader = null;
            }

            if (reader.HasRows && reader != null)
            {
                return reader;
            }
            else
            {
                CloseConnection();
                return null;
            }
        }

        public SqlDataReader GetReader(string procedure)
        {
            SqlDataReader reader;
            var command = GetCommandProcedure(procedure);

            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                reader = null;
            }

            if (reader.HasRows && reader != null)
            {
                return reader;
            }
            else
            {
                CloseConnection();
                return null;
            }
        }

        #endregion

        #region GetScalar
        public object GetScalar(string procedure, params SqlParameter[] parameters)
        {
            object scalar;

            var command = GetCommandProcedure(procedure);

            AddParameters(command, parameters);

            try
            {
                scalar = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                scalar = null;
            }
            CloseConnection();
            return scalar;
        }

        #endregion

        #region ExecuteNonQuery

        public int ExecuteNonQuery(string procedure, params SqlParameter[] parameters)
        {
            int result;

            var command = GetCommandProcedure(procedure);

            AddParameters(command, parameters);

            try
            {
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                result = 0;
            }

            CloseConnection();

            return result;
        }

        public int ExecuteNonQuery(string procedure, string parametrName, int? Id)
        {
            int result;

            var command = GetCommandProcedure(procedure);

            command.Parameters.AddWithValue(parametrName, Id);

            try
            {
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                result = 0;
            }

            CloseConnection();

            return result;

        }

        #endregion

        #region GetDataSet

        public DataSet GetDataSet(string procedure)
        {
            var adapter = GetAdapterProcedure(procedure);
            var dataSet = new DataSet();

            try
            {
                adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                dataSet = null;
            }
            CloseConnection();
            return dataSet;
        }

        #endregion

        #region CloseReader

        public void CloseReader(SqlDataReader reader)
        {
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            CloseConnection();
        }
        #endregion

        #region Conection

        private SqlConnection GetConnection()
        {
            _connection = new SqlConnection(_conStr);
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }

        private void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }


        #endregion

        #region GetCommandProcedure

        private SqlCommand GetCommandProcedure(string procedure)
        {
            return new SqlCommand(procedure, GetConnection())
            {
                CommandType = CommandType.StoredProcedure
            };
        }

        #endregion

        #region GetAdapterProcedure

        private SqlDataAdapter GetAdapterProcedure(string procedure)
        {
            return new SqlDataAdapter(GetCommandProcedure(procedure));
        }

        #endregion

        #region AddParameters

        private void AddParameters(SqlCommand command, params SqlParameter[] parameters)
        {
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.Add(item);
                }
            }
        }

        #endregion
    }
}