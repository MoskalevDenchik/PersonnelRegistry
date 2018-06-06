using System.ServiceModel.Configuration;
using DM.AdvertisingService.Contracts;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Configuration;
using DM.PR.Common.Helpers;
using DM.PR.Data.Core.Data;
using DM.PR.Common.Logger;
using DM.PR.Data.Entity;
using System.IO;
using System;

namespace DM.PR.Data.Core.Context.Implement
{
    public class WcfBillBoardContext : IDataContext<BillBoard>
    {
        private IAdService _service;
        private readonly IRecordLog _log;

        public WcfBillBoardContext(IRecordLog log)
        {
            Helper.ThrowExceptionIfNull(log);
            _log = log;
        }

        public IReadOnlyCollection<BillBoard> GetEntities(IInputParameter parameter)
        {
            try
            {
                _service = CreateChanel();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                _service = null;
            }


            AdvertisingService.Entities.BillBoard[] bor;
            try
            {
                bor = _service.GetRandomBiilBoards();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                return null;
            }

            var list = new List<Common.Entities.BillBoard>();

            foreach (var item in bor)
            {
                list.Add(new Common.Entities.BillBoard
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Image = item.Image,
                    Link = item.Link
                });
            }

            return list;
        }

        public BillBoard GetEntity(IInputParameter parameter)
        {
            throw new NotImplementedException();
        }

        public PagedData<BillBoard> GetPageEntities(IInputParameter parameter)
        {
            throw new NotImplementedException();
        }

        public void Save(IInputParameter parameter)
        {
            throw new NotImplementedException();
        }

        public IAdService CreateChanel()
        {
            var absolutePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "DM.PR.Data.dll.config");
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(
                 new ExeConfigurationFileMap { ExeConfigFilename = absolutePath }, ConfigurationUserLevel.None);
            var channelFactory =
                       new ConfigurationChannelFactory<IAdService>("BasicHttpBinding_IAdService", configuration, null);
            return channelFactory.CreateChannel();
        }
    }
}
