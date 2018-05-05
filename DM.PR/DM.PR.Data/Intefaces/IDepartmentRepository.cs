using DM.PR.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.PR.Data.Intefaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department Get(int? id);
        void Create(Department item);
        void Update(Department item);
        void Delete(int? id);
    }
}
