using DM.PR.Common.Logger;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DM.PR.Data.DataBase

{
    public class DataBase : IDataBase
    {
        #region Private

        private IRecordLog _log;
        private readonly string _conStr;

        #endregion

        #region Ctor

        public DataBase(IRecordLog log)
        {
            _log = log;

            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "DM.PR.Data.dll.config")
                };
                var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                _conStr = config.ConnectionStrings.ConnectionStrings["DataConnection"].ConnectionString;

            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                _conStr = null;
            }

        }


        #endregion

        #region GetEntity

        public T GetEntity<T>(Func<SqlDataReader, T> converter, string procedure, params SqlParameter[] parameters)
            where T : class, new()
        {
            var elemetn = new T();
            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    var command = GetComandWithParams(procedure, connection, parameters);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        elemetn = converter(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                elemetn = null;
            }

            return elemetn;
        }

        #endregion

        #region GetScalar                                                            

        public T GetScalar<T>(string procedure, params SqlParameter[] parameters)
              where T : new()
        {
            var result = new T();

            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    var command = GetComandWithParams(procedure, connection, parameters);

                    connection.Open();

                    result = (T)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
            }

            return result;
        }


        #endregion

        #region GetResult

        public int GetResult(string procedure, params SqlParameter[] parameters)
        {
            var result = 0;

            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    var command = GetComandWithParams(procedure, connection, parameters);

                    connection.Open();

                    result = command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
            }

            return result;

        }


        #endregion

        #region GetDataSet

        public DataSet GetDataSet(string procedure, params SqlParameter[] parameters)
        {
            var dataSet = new DataSet();

            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    var command = GetComandWithParams(procedure, connection, parameters);

                    var adapter = new SqlDataAdapter(command);
                    if (adapter != null)
                    {
                        connection.Open();
                        adapter.Fill(dataSet);
                    }
                    else
                    {
                        dataSet = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                dataSet = null;
            }
            return dataSet;
        }

        #endregion

        #region Helpers

        public SqlCommand GetComandWithParams(string procedure, SqlConnection connection, params SqlParameter[] parameters)
        {

            var command = new SqlCommand(procedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddRange(parameters);

            return command;
        }


        #endregion
    }
}