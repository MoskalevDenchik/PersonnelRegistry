using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using DM.PR.WEB.DependencyResolution;
using DM.PR.WEB.App_Start;
using System.Web.Mvc;
using WebActivatorEx;
using StructureMap;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace DM.PR.WEB.App_Start
{
    public static class StructuremapMvc
    {
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        public static void Start()
        {
            IContainer container = IoC.Initialize();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope);
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
        }
        public static void End()
        {
            StructureMapDependencyScope.Dispose();
        }
    }
}