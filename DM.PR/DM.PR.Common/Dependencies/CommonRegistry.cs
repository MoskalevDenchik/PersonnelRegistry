using DM.PR.Common.Logger;
using StructureMap.Configuration.DSL;

namespace DM.PR.Common.Dependencies
{
    public class CommonRegistry : Registry
    {

        public CommonRegistry()
        {
            ForSingletonOf<IRecordLog>().Use<RecordLog>();
        }
    }
}