using DM.PR.Business;
using DM.PR.Common;
using DM.PR.Data.DependencyResolution;
using StructureMap.Configuration.DSL;

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
                s.WithDefaultConventions();
                s.LookForRegistries();
            });
        }
    }
}