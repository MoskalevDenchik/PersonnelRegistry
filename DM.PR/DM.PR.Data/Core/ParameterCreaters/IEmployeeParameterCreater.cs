using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters
{
    internal interface IEmployeeParameterCreater
    {
        IInputParameter CreateFind(int pageSize, int page);

        IInputParameter CreateByDepartmentId(int departmentId, int pageSize, int page);

        IInputParameter CreateBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, int WorkStatusId, int pageSize, int page);
    }
}
