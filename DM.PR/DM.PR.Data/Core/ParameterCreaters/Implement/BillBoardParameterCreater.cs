using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Entities.Service;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    internal class BillBoardParameterCreater : IParameterCreater<BillBoard>
    {
        public IInputParameter CreateGetById(int id) => throw new NotImplementedException();

        public IInputParameter CreateGetAll() => new WcfInputParameter();

        public IInputParameter CreateSave(BillBoard item) => throw new NotImplementedException();

        public IInputParameter CreateRemove(int id) => throw new NotImplementedException();
    }
}
