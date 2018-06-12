using DM.PR.Common.Services.Implement;
using DM.PR.Common.Helpers.Implement;
using StructureMap.Configuration.DSL;
using DM.PR.Common.Services;
using DM.PR.Common.Helpers;
using DM.PR.Common.Logger;

namespace DM.PR.Common.Dependencies
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            ForSingletonOf<IRecordLog>().Use<RecordLog>();
            For<IConfigManger>().Use<ConfigManager>();
            ForSingletonOf<IÑachingService>().Use<ÑachingService>();
            For<IEnityReflector>().Use<EnityReflector>();
        }
    }
}