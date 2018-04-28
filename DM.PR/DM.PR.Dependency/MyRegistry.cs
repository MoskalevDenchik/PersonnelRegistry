namespace DM.PR.Dependency
{
    using StructureMap.Configuration.DSL;     
    using DM.PR.Business.Interfaces;
    using DM.PR.Business.Services;

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {                       
            For<IDepartmentServices>().Use<DepartmentServices>();
        }
    }
}