using System.Data;

namespace DM.PR.Data.Entity
{
    internal class DbInputParameter : IInputParameter
    {
        public string Procedure { get; set; }
        public IDbDataParameter[] Parameters { get; set; }
    }
}
