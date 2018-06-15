using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class MaritalStatusParameterCreater : IParameterCreater<MaritalStatus>
    {
        public override IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "GetMaritalStatusById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateForGetAll()
        {

            return new DbInputParameter
            {
                Procedure = "GetAllMaritalStatuses",
                Parameters = null
            };
        }

        public override IInputParameter CreateForAdd(MaritalStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertMaritalStatus",
                Parameters = { { "@Status", item.Status } }
            };
        }

        public override IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteMaritalStatus",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateForUpdate(MaritalStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateMaritalStatus",
                Parameters = { { "@Id", item.Id }, { "@Status", item.Status } }
            };
        }
    }
}
