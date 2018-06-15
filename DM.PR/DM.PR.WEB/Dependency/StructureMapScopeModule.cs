using StructureMap.Web.Pipeline;
using DM.PR.WEB.App_Start;
using System.Web;

namespace DM.PR.WEB.DependencyResolution
{
    public class StructureMapScopeModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) =>
            {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        public void Dispose() { }
    }
}