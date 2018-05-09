using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using DM.PR.Data.AdServiceClient;
using DM.PR.Data.Intefaces;

namespace DM.PR.Data.Repositories
{
    public class AdRepository : IAdRepository
    {
        public IEnumerable<string> GetAll()
        {
            //ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            //{
            //    ExeConfigFilename = "app.config"
            //};

            //Configuration newConfiguration = ConfigurationManager.OpenMappedExeConfiguration(
            //    fileMap,
            //    ConfigurationUserLevel.None);

            //ConfigurationChannelFactory<IAdService> channel =
            //    new ConfigurationChannelFactory<IAdService>("BasicHttpBinding_IAdService", newConfiguration, null);
            //ICalculatorChannel client1 = factory1.CreateChannel();

            ChannelFactory<IAdService> channel =
                new ChannelFactory<IAdService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:49584/AdService.svc"));

            IAdService adService = channel.CreateChannel();

            return adService.GetContent();

        }
    }
}
