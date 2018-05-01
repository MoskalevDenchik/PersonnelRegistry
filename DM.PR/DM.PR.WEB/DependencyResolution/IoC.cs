
namespace DM.PR.WEB.DependencyResolution
{
    using StructureMap;
    using DM.PR.Dependency;

    public static class IoC
    {
        public static IContainer Initialize()
        {
            return new Container(c =>
            {
                c.AddRegistry<MyRegistry>();
                c.AddRegistry<DefaultRegistry>();
            });
        }
    }
}