namespace DM.PR.Dependency
{
    using StructureMap.Configuration.DSL;
    using DM.PR.Business.Interfaces;
    using DM.PR.Business.Services;
    using DM.PR.Common.Logger;

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            For<IDepartmentServices>().Use<DepartmentServices>();
            ForSingletonOf<IRecordLog>().Use<WorkLogger>();
        }
    }
}