using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class KindPhoneParameterCreater : ParameterCreater<KindPhone>
    {
        public override IInputParameter CreateGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectKindPhoneById",
                Parameters = { { "@Id", id } }
            };
        }

        public override IInputParameter CreateGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllKindPhones",
                Parameters = null
            };
        }

        public override IInputParameter CreateAdd(KindPhone item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertKindPhone",
                Parameters = { { "@Kind", item.Kind } }
            };
        }
        public override IInputParameter CreateUpdate(KindPhone item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateKindPhone",
                Parameters = { { "@Id", item.Id }, { "@Kind", item.Kind } }
            };
        }

        public override IInputParameter CreateRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteKindPhone",
                Parameters = { { "@Id", id } }
            };
        }
    }
}
