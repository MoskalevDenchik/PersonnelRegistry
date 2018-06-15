using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class RoleParameterCreater : ParameterCreater<Role>
    {
        public override IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectRoleById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllRoles",
                Parameters = null
            };
        }

        public override IInputParameter CreateAdd(Role item)
        {
            throw new NotImplementedException();
        }
        public override IInputParameter CreateUpdate(Role item)
        {
            throw new NotImplementedException();
        }

        public override IInputParameter CreateRemove(int id)
        {
            throw new NotImplementedException();
        }

    }
}
