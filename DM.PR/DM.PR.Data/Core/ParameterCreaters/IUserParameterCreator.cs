using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters
{
    internal interface IUserParameterCreator
    {
        IInputParameter CreateByLogin(string login);
        IInputParameter CreateByEmployeeId(int employeeId);
    }
}
