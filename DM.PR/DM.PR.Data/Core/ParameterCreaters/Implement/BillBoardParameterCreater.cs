using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Entities.Service;
using DM.PR.Data.Specifications;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class BillBoardParameterCreater : IParameterCreater<BillBoard>
    {
        public IInputParameter CreateForGetById(int id)
        {
            throw new NotImplementedException();
        }
        public IInputParameter CreateForGetAll()
        {
            return new WcfInputParameter();
        }

        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(BillBoard item)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForRemove(int id)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForUpdate(BillBoard item)
        {
            throw new NotImplementedException();
        }
    }
}
