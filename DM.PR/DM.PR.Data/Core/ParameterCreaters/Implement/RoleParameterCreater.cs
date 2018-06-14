using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities.Account;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Data.Entity;
using System;                        

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class RoleParameterCreater : IParameterCreater<Role>
    {
        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectRoleById",
                Parameters = new SqlParameter[]
                 {
                    new SqlParameter("@Id", id)
                 }
            };
        }

        public IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllRoles",
                Parameters = null
            };
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(Role item)
        {
            throw new NotImplementedException();
        }
        public IInputParameter CreateForUpdate(Role item)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForRemove(int id)
        {
            throw new NotImplementedException();
        }

    }
}
