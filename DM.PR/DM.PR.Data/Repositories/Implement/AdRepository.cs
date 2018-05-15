using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel.Configuration;
using DM.PR.Common.Logger;
using DM.PR.Data.AdServiceClient;

namespace DM.PR.Data.Repositories.Implement
{
    public class AdRepository : IAdRepository
    {
        #region Private
        private IAdService _service;
        private IRecordLog _log;

        #endregion

        #region Ctor

        public AdRepository(IRecordLog log)
        {
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

        #endregion

        #region GetAll

        public IReadOnlyCollection<string> GetAll()
        {
            try
            {
                return _service.GetContent();
            }
            catch (Exception ex)
            {
                _log.MakeInfo(ex.Message);
                return null;
            }
        }

        #endregion

        #region Helpers

        public IAdService CreateChanel()
        {
            var absolutePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "DM.PR.Data.dll.config");
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(
                 new ExeConfigurationFileMap { ExeConfigFilename = absolutePath }, ConfigurationUserLevel.None);
            var channelFactory =
                       new ConfigurationChannelFactory<IAdService>("BasicHttpBinding_IAdService", configuration, null);
            return channelFactory.CreateChannel();
        }

        #endregion
    }
}
