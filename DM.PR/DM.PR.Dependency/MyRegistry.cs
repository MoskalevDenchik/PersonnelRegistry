namespace DM.PR.Dependency
{
    using StructureMap.Configuration.DSL;
    using DM.PR.Business.Interfaces;  
    using DM.PR.Common.Logger;
    using DM.PR.Business.Providers;

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            For<IDepartmentProvider>().Use<DepartmentProvider>();
            ForSingletonOf<IRecordLog>().Use<WorkLogger>();
        }
    }
}