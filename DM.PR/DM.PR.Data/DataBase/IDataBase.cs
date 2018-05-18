using System;
using System.Data;
using System.Data.SqlClient;

namespace DM.PR.Data.DataBase
{
    public interface IDataBase
    {
        T GetScalar<T>(string procedure, params SqlParameter[] parameters) where T :  new();
        T GetEntity<T>(Func<SqlDataReader, T> converter, string procedure, params SqlParameter[] parameters) where T : class, new();
        int GetResult(string procedure, params SqlParameter[] parameters);
        DataSet GetDataSet(string procedure, params SqlParameter[] parameters);
    }
}
