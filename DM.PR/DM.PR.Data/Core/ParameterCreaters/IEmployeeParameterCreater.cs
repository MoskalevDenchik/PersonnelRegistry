using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters
{
    internal interface IEmployeeParameterCreater
    {
        IInputParameter CreateForFindByPageData(int pageSize, int page);

        IInputParameter CreateForFindPageByDepartmentId(int departmentId, int pageSize, int page);

        IInputParameter CreateForFindPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, bool IsWorking, int pageSize, int page);
    }
}
