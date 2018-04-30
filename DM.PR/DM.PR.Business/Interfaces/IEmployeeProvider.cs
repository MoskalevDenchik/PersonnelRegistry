using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Business.Interfaces
{
    public interface IEmployeeProvider
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> FindAllByDepartmentName(string name);
    }
}
