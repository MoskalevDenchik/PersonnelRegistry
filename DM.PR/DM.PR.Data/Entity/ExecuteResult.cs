
namespace DM.PR.Data.Entity
{
    public class ExecuteResult
    {
        private object _result;
        public ExecuteResult(object result)
        {
            _result = result;
            if (_result == null)
            {
                IsNull = true;
            }
        }
        public ExecuteResult()
        {

        }
        public object Result => _result;
        public bool IsNull { get; } = false;
        public int Id { get; set; }
        public int Rows { get; set; }
    }
}
