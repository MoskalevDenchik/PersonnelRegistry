using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Business.Interfaces
{
    public interface IDepartmentServices
    {
        IEnumerable<Department> GetAll();

        IEnumerable<string> GetListOfName();


    }
}
