using System.Data;
using System.Data.SqlClient;       

namespace DM.PR.Data.DataBase
{
    public interface IDataBase
    {
        SqlDataReader GetReader(string procedure, string parametrName, int? Id);
        SqlDataReader GetReader(string procedure, params SqlParameter[] parameters);
        SqlDataReader GetReader(string procedure);
        object GetScalar(string procedure, params SqlParameter[] parameters);
        int ExecuteNonQuery(string procedure, params SqlParameter[] parameters);
        int ExecuteNonQuery(string procedure, string parametrName, int? Id);
        DataSet GetDataSet(string procedure);
        void CloseReader(SqlDataReader reader);
    }
}
