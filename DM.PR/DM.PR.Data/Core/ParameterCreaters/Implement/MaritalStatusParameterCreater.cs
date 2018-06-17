using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class MaritalStatusParameterCreater : IParameterCreater<MaritalStatus>
    {
        public IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "GetMaritalStatusById",
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
                Procedure = "GetAllMaritalStatuses"
            };
        }

        public IInputParameter CreateSave(MaritalStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "SaveMaritalStatus",
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
                Procedure = "DeleteMaritalStatus",
                Parameters =
                {
                    {nameof(id), id }
                }
            };
        }
    }
}
