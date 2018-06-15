using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Entities.Service;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class BillBoardParameterCreater : IParameterCreater<BillBoard>
    {
        public override IInputParameter CreateForGetById(int id)
        {
            throw new NotImplementedException();
        }
        public override IInputParameter CreateForGetAll()
        {
            return new WcfInputParameter();
        }

        public override IInputParameter CreateForAdd(BillBoard item)
        {
            throw new NotImplementedException();
        }

        public override IInputParameter CreateForRemove(int id)
        {
            throw new NotImplementedException();
        }

        public override IInputParameter CreateForUpdate(BillBoard item)
        {
            throw new NotImplementedException();
        }
    }
}
