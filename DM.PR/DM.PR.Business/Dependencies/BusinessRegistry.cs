using DM.PR.Business.Providers.Implement;
using DM.PR.Business.Services.Implement;
using StructureMap.Configuration.DSL;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;

namespace DM.PR.Business.Dependencies
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IUserProvider>().Use<UserProvider>();
            For<IEmployeeProvider>().Use<EmployeeProvider>();
            For<IBillBoardProvider>().Use<BillBoardProvider>();
            For<IKindPhoneProvider>().Use<KindPhoneProvider>();
            For<IDepartmentProvider>().Use<DepartmentProvider>();
            For<IMaritalStatusProvider>().Use<MaritalStatusProvider>();

            For<IUserService>().Use<UserService>();
            For<ILoginServices>().Use<LoginServices>();
            For<IEmployeeService>().Use<EmployeeService>();
            For<IKindPhoneService>().Use<KindPhoneService>();
            For<IDepartmentService>().Use<DepartmentService>();
            For<IMaritalStatusService>().Use<MaritalStatusService>();
        }
    }
}