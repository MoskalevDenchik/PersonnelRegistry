using System.Data.SqlClient;

namespace DM.PR.Data.Core.Parameters
{
    public static class UserParameters
    {
        public static SqlParameter ById(int id)
        {
            return new SqlParameter("@Id", id);
        }
        public static SqlParameter ByLogin(string login)
        {
            return new SqlParameter("@Login", login);
        }
    }
}
