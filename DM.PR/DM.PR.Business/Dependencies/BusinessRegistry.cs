using DM.PR.Business.Providers;
using DM.PR.Business.Providers.Implement;
using DM.PR.Business.Services;
using DM.PR.Business.Services.Implement;
using StructureMap.Configuration.DSL;

namespace DM.PR.Business.Dependencies
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IAdProvider>().Use<AdProvider>();
            For<IEmployeeProvider>().Use<EmployeeProvider>();
            For<IKindPhoneProvider>().Use<KindPhoneProvider>();
            For<IDepartmentProvider>().Use<DepartmentProvider>();
            For<IMaritalStatusProvider>().Use<MaritalStatusProvider>();

            For<ILoginServices>().Use<LoginServices>();
            For<IEmployeeService>().Use<EmployeeService>();
            For<IDepartmentService>().Use<DepartmentService>();
        }

    }
}