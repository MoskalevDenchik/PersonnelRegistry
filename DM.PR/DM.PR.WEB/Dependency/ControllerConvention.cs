using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;
using StructureMap.Pipeline;
using StructureMap.Graph;
using System.Web.Mvc;
using System;

namespace DM.PR.WEB.DependencyResolution
{
    public class ControllerConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.CanBeCastTo<Controller>() && !type.IsAbstract)
            {
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }
    }
}