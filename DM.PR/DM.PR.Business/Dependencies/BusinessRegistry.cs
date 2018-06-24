using DM.PR.Business.Providers.Implement;
using DM.PR.Business.Services.Implement;
using StructureMap.Configuration.DSL;
using DM.PR.Common.Entities.Account;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;

namespace DM.PR.Business.Dependencies
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IUserProvider>().Use<UserProvider>();
            For<IProvider<Role>>().Use<Provider<Role>>();
            For<IEmployeeProvider>().Use<EmployeeProvider>();
            For<IProvider<BillBoard>>().Use<BillBoardProvider>();
            For<IDepartmentProvider>().Use<DepartmentProvider>();
            For<IProvider<WorkStatus>>().Use<Provider<WorkStatus>>();
            For<IProvider<MaritalStatus>>().Use<Provider<MaritalStatus>>();

            For<ILoginServices>().Use<LoginServices>();
            For<IEntityService<User>>().Use<UserService>();
            For<IEntityService<WorkStatus>>().Use<WorkStatusService>();
            For<IEntityService<Employee>>().Use<EntityService<Employee>>();
            For<IEntityService<MaritalStatus>>().Use<MaritalStatusService>();
            For<IEntityService<Department>>().Use<EntityService<Department>>();
        }
    }
}