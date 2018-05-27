using System;
using System.Configuration;
using System.IO;

namespace DM.PR.Common.Helpers.Implement
{
    public class ConfigManager : IConfigManger
    {
        public string GetConnectionString(string connectionName)
        {
            var config = GetConfigFile("DM.PR.Data.dll.config");

            return config.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString;

        }
        private Configuration GetConfigFile(string dllName)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap
            {
                ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, dllName)
            };
            return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

    }
}
