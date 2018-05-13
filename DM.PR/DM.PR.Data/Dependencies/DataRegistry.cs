using DM.PR.Data.DataBase;
using DM.PR.Data.Repositories;
using DM.PR.Data.Repositories.Implement;
using StructureMap.Configuration.DSL;

namespace DM.PR.Data.Dependencies
{
    public class DataRegistry : Registry
    {

        public DataRegistry()
        {
            For<IDataBase>().Use<DataBase.DataBase>();

            For<IAdRepository>().Use<AdRepository>();
            For<IEmployeeRepository>().Use<EmployeeRepository>();
            For<IDepartmentRepository>().Use<DepartmentRepository>();
        }

    }
}