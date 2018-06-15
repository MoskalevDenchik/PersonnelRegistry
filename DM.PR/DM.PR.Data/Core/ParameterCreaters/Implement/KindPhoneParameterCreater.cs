using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class KindPhoneParameterCreater : IParameterCreater<KindPhone>
    {
        public override IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectKindPhoneById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllKindPhones",
                Parameters = null
            };
        }

        public override IInputParameter CreateForAdd(KindPhone item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertKindPhone",
                Parameters = { { "@Kind", item.Kind } }
            };
        }
        public override IInputParameter CreateForUpdate(KindPhone item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateKindPhone",
                Parameters = { { "@Id", item.Id }, { "@Kind", item.Kind } }
            };
        }

        public override IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteKindPhone",
                Parameters = { { "@Id", id } }
            };
        }
    }
}
