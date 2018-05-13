using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using DM.PR.Common.Logger;
using DM.PR.Data.AdServiceClient;   

namespace DM.PR.Data.Repositories.Implement
{
    public class AdRepository : IAdRepository
    {
        private IRecordLog _log;
        private IAdService _service;
        private ChannelFactory<IAdService> channelFactory;

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
    
        public IReadOnlyCollection<string> GetAll()
        {
            
            if (_service != null && channelFactory.State == CommunicationState.Opened )
            {
                return _service.GetContent();
            }
            else
            {
                return new List<string>
                {
                    "Мы против рекламы"
                };
            }
        }

        #region CreateChanel
        private IAdService CreateChanel()
        {
            string absolutePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "App.config");

            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(
                new ExeConfigurationFileMap { ExeConfigFilename = absolutePath }, ConfigurationUserLevel.None);

            channelFactory =
                new ConfigurationChannelFactory<IAdService>("BasicHttpBinding_IAdService", configuration, null);

            return channelFactory.CreateChannel();
        }


        #endregion
    }
}
