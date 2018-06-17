using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class WorkStatusParameterCreater : IParameterCreater<WorkStatus>
    {
        public IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectWorkStatusById",
                Parameters =
                {
                    {nameof(id), id}
                }
            };
        }

        public IInputParameter CreateGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllWorkStatus"
            };
        }

        public IInputParameter CreateSave(WorkStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "SaveWorkStatus",
                Parameters =
                {
                    {nameof(item.Id), item.Id},
                    {nameof(item.Status), item.Status}
                }
            };
        }

        public IInputParameter CreateRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteWorkStatus",
                Parameters =
                {
                    {nameof(id), id}
                }
            };
        }
    }
}
