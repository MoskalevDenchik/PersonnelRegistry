using DM.PR.Data.Entity;
using System;                      
using System.Data.SqlClient;

namespace DM.PR.Data.Core.Data
{
    internal interface IDbAccess
    {
        T ExecuteCommand<T>(Func<SqlCommand, T> func, string procedure, params SqlParameter[] parameters);
    }
}
