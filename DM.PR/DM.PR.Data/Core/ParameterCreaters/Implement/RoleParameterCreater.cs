using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class RoleParameterCreater : IParameterCreater<Role>
    {
        public IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectRoleById",
                Parameters =
                {
                    {nameof(id), id }
                }
            };
        }

        public IInputParameter CreateGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllRoles"
            };
        }

        public IInputParameter CreateSave(Role item)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateRemove(int id)
        {
            throw new NotImplementedException();
        }

    }
}
