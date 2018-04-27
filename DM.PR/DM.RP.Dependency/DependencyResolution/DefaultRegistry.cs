namespace DM.RP.Dependency.DependencyResolution {
    using DM.PR.Business.Interfaces;
    using DM.PR.Business.Services;
    using DM.PR.WEB.DependencyResolution;
    using StructureMap;  
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });
            For<IDepartmentServices>().Use<DepartmentServices>();
        }
        #endregion
    }
}