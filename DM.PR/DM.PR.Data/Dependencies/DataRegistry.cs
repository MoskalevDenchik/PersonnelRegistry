using DM.PR.Common.Entities;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Core.Converters;
using DM.PR.Data.Core.Converters.Implement;
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

            For<IConverter<User>>().Use<UserConverter>();
            For<IConverter<Employee>>().Use<EmployeeConverter>();
            For<IConverter<KindPhone>>().Use<KindPhoneConverter>();
            For<IConverter<Department>>().Use<DepartmentConverter>();
            For<IConverter<MaritalStatus>>().Use<MaritalStatusConverter>();

            For<IUserRepository>().Use<UserRepository>();
            For<IKindPhoneRepository>().Use<KindPhoneRepository>();
            For<IAdRepository>().Use<AdRepository>();
            For<IEmployeeRepository>().Use<EmployeeRepository>();
            For<IDepartmentRepository>().Use<DepartmentRepository>();
            For<IMaritalStatusRepository>().Use<MaritalStatusRepository>();
        }
    }
}