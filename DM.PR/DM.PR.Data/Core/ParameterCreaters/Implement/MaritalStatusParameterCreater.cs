using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

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
            return new DbInputParameter
            {
                Procedure = "InsertMaritalStatus",
                Parameters = new SqlParameter[]
                 {
                    new SqlParameter("@Status", item.Status)
                 }
            };
        }

        public IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteMaritalStatus",
                Parameters = new SqlParameter[]
               {
                    new SqlParameter("@Id", id)
               }
            };
        }

        public IInputParameter CreateForUpdate(MaritalStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateMaritalStatus",
                Parameters = new SqlParameter[]
                 {
                    new SqlParameter("@Id", item.Id),
                    new SqlParameter("@Status", item.Status)
                 }
            };
        }
    }
}
