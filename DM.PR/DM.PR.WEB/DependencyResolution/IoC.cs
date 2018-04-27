
namespace DM.PR.WEB.DependencyResolution {
    using StructureMap;
    using DM.PR.Dependency.DependencyResolution;
	
    public static class IoC {
        public static IContainer Initialize() {
            return new Container(c => c.AddRegistry<DefaultRegistry>());
        }
    }
}