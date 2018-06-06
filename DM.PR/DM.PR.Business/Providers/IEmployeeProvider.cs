using DM.PR.Common.Entities;

namespace DM.PR.Business.Providers
{
    public interface IEmployeeProvider
    {
        Employee GetById(int id);
        PagedData<Employee> GetPage(int pageSize, int page);
        PagedData<Employee> GetPageByDepartmentId(int departmentId, int pageSize, int page);
        PagedData<Employee> GetPageBySearchParams(string lastName, string firstName, string middledName, int fromYear, int toYear, bool IsWorking, int pageSize, int page);
    }
}
