using DM.PR.Data.Core.Data;
using DM.PR.Data.Core.Data.Implement;
using DM.PR.Data.Repositories;
using DM.PR.Data.Repositories.Implement;
using StructureMap.Configuration.DSL;

namespace DM.PR.Data.Dependencies
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IDbAccess>().Use<DbAccess>();
            For<IDbExecutor>().Use<DbExecutor>();

            For<IKindPhoneRepository>().Use<KindPhoneRepository>();
            For<IAdRepository>().Use<AdRepository>();
            For<IEmployeeRepository>().Use<EmployeeRepository>();
            For<IDepartmentRepository>().Use<DepartmentRepository>();
            For<IMaritalStatusRepository>().Use<MaritalStatusRepository>();
        }
    }
}