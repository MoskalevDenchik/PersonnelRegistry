using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    class KindPhoneParameterCreater : IParameterCreater<KindPhone>
    {
        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectKindPhoneById",
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
                Procedure = "SelectAllKindPhones",
                Parameters = null
            };
        }
        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(KindPhone item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertKindPhone",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Kind", item.Kind)
                }
            };
        }
        public IInputParameter CreateForUpdate(KindPhone item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateKindPhone",
                Parameters = new SqlParameter[]
               {
                    new SqlParameter("@Id", item.Id),
                    new SqlParameter("@Kind", item.Kind)
               }
            };
        }

        public IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteKindPhone",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                }
            };
        }

    }
}
