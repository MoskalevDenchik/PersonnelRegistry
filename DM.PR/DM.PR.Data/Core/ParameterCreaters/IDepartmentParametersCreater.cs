using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.InputParameters.Creaters
{
    internal interface IDepartmentParameterCreater
    {
        IInputParameter CreateFind(int pageSize, int page);
    }
}

