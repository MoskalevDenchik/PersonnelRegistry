using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel.Configuration;
using DM.AdvertisingService.Contracts;
using DM.AdvertisingService.Entities;
using DM.PR.Common.Helpers;
using DM.PR.Common.Logger;

namespace DM.PR.Data.Repositories.Implement
{
    internal class AdRepository : IAdRepository
    {
        private readonly IAdService _service;
        private readonly IRecordLog _log;

        public AdRepository(IRecordLog log)
        {
            Helper.ThrowExceptionIfNull(log);
            _log = log;

            try
            {
                _service = CreateChanel();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                _service = null;
            }
        }

        public IReadOnlyCollection<Common.Entities.BillBoard> GetAll()
        {
            BillBoard[] bor;
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
