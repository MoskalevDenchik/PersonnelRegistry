using DM.PR.Business.Interfaces;
using DM.PR.Business.Providers;
using DM.PR.Business.Services;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace DM.PR.Business
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            //For<IEmployeeService>().Use<EmployeeService>();
            //For<IDepartmentService>().Use<DepartmentService>();

            //For<IEmployeeProvider>().Use<EmployeeProvider>();
            //For<IDepartmentProvider>().Use<DepartmentProvider>();
        }

    }
}