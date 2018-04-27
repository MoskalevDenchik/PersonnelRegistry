namespace DM.PR.Dependency.DependencyResolution {
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph; 
    using DM.PR.Business.Interfaces;
    using DM.PR.Business.Services;

    public class DefaultRegistry : Registry {
              public DefaultRegistry() {
     //       Scan(
     //           scan => {
     //               scan.TheCallingAssembly();
     //               scan.WithDefaultConventions();
					//scan.With(new ControllerConvention());
     //           });
            For<IDepartmentServices>().Use<DepartmentServices>();
        }     
    }
}