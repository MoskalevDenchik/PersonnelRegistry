using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class RoleParameterCreater : IParameterCreater<Role>
    {
        public override IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectRoleById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllRoles",
                Parameters = null
            };
        }

        public override IInputParameter CreateForAdd(Role item)
        {
            throw new NotImplementedException();
        }
        public override IInputParameter CreateForUpdate(Role item)
        {
            throw new NotImplementedException();
        }

        public override IInputParameter CreateForRemove(int id)
        {
            throw new NotImplementedException();
        }

    }
}
