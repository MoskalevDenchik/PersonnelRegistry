using DM.PR.Common.Logger;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace DM.PR.Common
{
    public class CommonRegistry : Registry
    {

        public CommonRegistry()
        {
            ForSingletonOf<IRecordLog>().Use<RecordLog>();
        }
    }
}