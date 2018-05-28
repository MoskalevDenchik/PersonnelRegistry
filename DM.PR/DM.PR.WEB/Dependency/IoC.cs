using DM.PR.Dependency;
using StructureMap;

namespace DM.PR.WEB.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            return new Container(c =>
            {
                c.AddRegistry<DefaultRegistry>();
                c.AddRegistry<GeneralRegistry>();
            });
        }
    }
}