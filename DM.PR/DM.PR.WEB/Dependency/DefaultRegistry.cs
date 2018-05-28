using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace DM.PR.WEB.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
        }
    }
}