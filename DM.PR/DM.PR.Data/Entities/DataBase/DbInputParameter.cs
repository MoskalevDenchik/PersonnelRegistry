using System.Collections.Generic;

namespace DM.PR.Data.Entity
{
    internal class DbInputParameter : IInputParameter
    {
        private Dictionary<string, object> _parameters = new Dictionary<string, object>();
        public string Procedure { get; set; }
        public Dictionary<string, object> Parameters
        {
            get => _parameters;
            set
            {
                _parameters = value;
            }
        }
    }
}
