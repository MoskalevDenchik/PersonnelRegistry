using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.InputParameters.Creaters
{
    internal interface IDepartmentParameterCreater
    {
        IInputParameter CreateForFindByPageData(int pageSize, int page);
    }
}

