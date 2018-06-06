using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class MaritalStatusParameterCreater : IParameterCreater<MaritalStatus>
    {
        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "GetMaritalStatusById",
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
                Procedure = "GetAllMaritalStatuses",
                Parameters = null
            };
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }
        public IInputParameter CreateForAdd(MaritalStatus item)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForRemove(int id)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForUpdate(MaritalStatus item)
        {
            throw new NotImplementedException();
        }
    }
}
