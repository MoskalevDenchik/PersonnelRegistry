using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class WorkStatusParameterCreater : IParameterCreater<WorkStatus>
    {
        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectWorkStatusById",
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
                Procedure = "SelectAllWorkStatus",
                Parameters = null
            };
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(WorkStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "InsertWorkStatus",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Status", item.Status)
                }
            };
        }

        public IInputParameter CreateForUpdate(WorkStatus item)
        {
            return new DbInputParameter
            {
                Procedure = "UpdateWorkStatus",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", item.Id),
                    new SqlParameter("@Status", item.Status)
                }
            };
        }

        public IInputParameter CreateForRemove(int id)
        {
            return new DbInputParameter
            {
                Procedure = "DeleteWorkStatus",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                }
            };
        }
    }
}
