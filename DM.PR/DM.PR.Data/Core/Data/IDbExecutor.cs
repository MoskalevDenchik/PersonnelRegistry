using DM.PR.Data.Entity;           
using System.Data.SqlClient;    
                                                       
namespace DM.PR.Data.Core.Data
{
    internal interface IDbExecutor
    {
        ExecuteResult Execute(string procedure, ResultType result = ResultType.DataSet, params SqlParameter[] parameters);
    }
}
