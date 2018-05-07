using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Business.Interfaces
{
    public interface IAdProvider
    {
        IEnumerable<string> GetContent();
    }
}
