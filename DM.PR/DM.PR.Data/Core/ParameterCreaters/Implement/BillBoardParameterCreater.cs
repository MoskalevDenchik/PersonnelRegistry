using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Entities.Service;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class BillBoardParameterCreater : ParameterCreater<BillBoard>
    {
        public override IInputParameter CreateGetById(int id)
        {
            throw new NotImplementedException();
        }
        public override IInputParameter CreateGetAll()
        {
            return new WcfInputParameter();
        }

        public override IInputParameter CreateAdd(BillBoard item)
        {
            throw new NotImplementedException();
        }

        public override IInputParameter CreateRemove(int id)
        {
            throw new NotImplementedException();
        }

        public override IInputParameter CreateUpdate(BillBoard item)
        {
            throw new NotImplementedException();
        }
    }
}
