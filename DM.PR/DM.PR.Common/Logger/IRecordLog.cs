using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Common.Logger
{
    public interface IRecordLog
    {
        void MakeInfo(string message);

        void MaleError(object ex);

    }
}
