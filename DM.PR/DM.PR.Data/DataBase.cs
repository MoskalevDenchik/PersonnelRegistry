using System;                      
using System.Data;
using System.Data.SqlClient; 

namespace DM.PR.Data
{
    public class DataBase
    {
        private const string _conStr = @"Data Source=DESKTOP-1OSBSDG\SQLEXPRESS;Initial Catalog=PersonnelDB;Integrated Security=True";
        private static SqlConnection _connection;

        public static SqlConnection GetConnection()
        {
            _connection = new SqlConnection(_conStr);
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }

        public static SqlDataReader ExecuteReader(string pocedure, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(pocedure, GetConnection())
            {
                CommandType = CommandType.StoredProcedure

            };

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.Add(item);
                }
            }
            return command.ExecuteReader();
        }
        public static int ExecuteScalar(string pocedure, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(pocedure, GetConnection())
            {
                CommandType = CommandType.StoredProcedure

            };

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.Add(item);
                }
            }
          
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public static void ExecuteNonQuery(string pocedure, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(pocedure, GetConnection())
            {
                CommandType = CommandType.StoredProcedure

            };

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.Add(item);
                }
            }
            command.ExecuteNonQuery();
        }




        public static void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

    }


}