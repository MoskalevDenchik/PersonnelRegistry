using StructureMap.Configuration.DSL;
using DM.PR.Business.Dependencies;
using DM.PR.Common.Dependencies;
using DM.PR.Data.Dependencies;


namespace DM.PR.Dependency
{
    public class GeneralRegistry : Registry
    {
        public GeneralRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<BusinessRegistry>();
                s.AssemblyContainingType<CommonRegistry>();
                s.AssemblyContainingType<DataRegistry>();   
                s.LookForRegistries();
            });
        }
    }
}