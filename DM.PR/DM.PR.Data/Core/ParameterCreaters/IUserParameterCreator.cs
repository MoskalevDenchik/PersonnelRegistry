using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters
{
    internal interface IUserParameterCreator
    {
        IInputParameter CreateForFindByLogin(string login);
    }
}
