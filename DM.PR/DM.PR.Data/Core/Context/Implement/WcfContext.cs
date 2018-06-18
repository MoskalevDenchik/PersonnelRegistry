using System.ServiceModel.Configuration;
using DM.AdvertisingService.Contracts;
using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Configuration;
using DM.PR.Common.Helpers;
using DM.PR.Data.Core.Data;
using DM.PR.Common.Logger;
using DM.PR.Data.Entity;
using System.Linq;
using System.IO;
using System;
using System.ServiceModel;

namespace DM.PR.Data.Core.Context.Implement
{
    public class WcfBillBoardContext : IDataContext<BillBoard>
    {
        #region Private

        private IAdService _adClient;
        private readonly IRecordLog _log;

        #endregion

        #region Ctors

        public WcfBillBoardContext(IRecordLog log)
        {
            Inspector.ThrowExceptionIfNull(log);
            _log = log;
        }

        #endregion

        public IReadOnlyCollection<BillBoard> GetEntities(IInputParameter parameter)
        {
            AdvertisingService.Entities.BillBoard[] borders = null;
            ChannelFactory<IAdService> factory = CreateFactory();
            try
            {
                _adClient = factory.CreateChannel();
                borders = _adClient.GetRandomBiilBoards();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                return null;
            }
            finally
            {
                ((IClientChannel)_adClient).Close();
                factory.Close();
            }

            List<BillBoard> list = null;

            if (borders != null)
            {
                list = borders.Select(x => new BillBoard
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    Link = x.Link
                }).ToList();
            }

            return list;
        }

        public BillBoard GetEntity(IInputParameter parameter) => throw new NotImplementedException();

        public void Save(IInputParameter parameter) => throw new NotImplementedException();

        public IReadOnlyCollection<BillBoard> GetEntities(IInputParameter parameter, out int outputParameter) => throw new NotImplementedException();

        #region Helpers

        public ChannelFactory<IAdService> CreateFactory()
        {
            var absolutePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "DM.PR.Data.dll.config");
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(
                 new ExeConfigurationFileMap { ExeConfigFilename = absolutePath }, ConfigurationUserLevel.None);
            return new ConfigurationChannelFactory<IAdService>("BasicHttpBinding_IAdService", configuration, null);
        }

        #endregion
    }
}
