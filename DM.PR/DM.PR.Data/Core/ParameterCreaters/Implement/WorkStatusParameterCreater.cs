using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class WorkStatusParameterCreater : IParameterCreater<WorkStatus>
    {
        public override IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectWorkStatusById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllWorkStatus",
                Parameters = null
            };
        }
        
        public override IInputParameter CreateForAdd(WorkStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertWorkStatus",
                Parameters = { { "@Status", item.Status } }
            };
        }

        public override IInputParameter CreateForUpdate(WorkStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateWorkStatus",
                Parameters =
                {
                    {"@Id", item.Id},
                    {"@Status", item.Status}
                }
            };
        }

        public override  IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteWorkStatus",
                Parameters = { { "@Id", id } }
            };
        }
    }
}
