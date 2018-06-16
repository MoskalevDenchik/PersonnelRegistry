using DM.PR.Business.Providers.Implement;
using DM.PR.Business.Services.Implement;
using StructureMap.Configuration.DSL;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using DM.PR.Common.Entities;
using DM.PR.Common.Entities.Account;

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
            For<IProvider<KindPhone>>().Use<Provider<KindPhone>>();
            For<IProvider<WorkStatus>>().Use<Provider<WorkStatus>>();
            For<IProvider<MaritalStatus>>().Use<Provider<MaritalStatus>>();

            For<IUserService>().Use<UserService>();
            For<ILoginServices>().Use<LoginServices>();
            For<IEmployeeService>().Use<EmployeeService>();
            For<IKindPhoneService>().Use<KindPhoneService>();
            For<IWorkStatusService>().Use<WorkStatusService>();
            For<IDepartmentService>().Use<DepartmentService>();
            For<IMaritalStatusService>().Use<MaritalStatusService>();
        }
    }
}