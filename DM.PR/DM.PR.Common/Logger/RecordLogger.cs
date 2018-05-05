using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace DM.PR.Common.Logger
{
    public class RecordLog : IRecordLog
    {
        private readonly ILog _log;

        public RecordLog()
        {
            InitLogger();
            _log = LogManager.GetLogger("MyLogger");
        }

        public void MakeInfo(string message)
        {
            _log.Info(message);
        }

        public void MaleError(object message)
        {
            _log.Error(message);
        }

        private void InitLogger()
        {
            if (!LogManager.GetRepository().Configured)
            {
                // имя файла конфигурации
                string configFileName = "Log";

                // находи текущую деректорию
                string curentDirectory = GetFullPathToConfigFile();

                // делаем полный адрес к файлу          
                FileInfo fullPath = new FileInfo($"{curentDirectory}\\{configFileName}.config");

                if (fullPath.Exists)
                {
                    XmlConfigurator.Configure(fullPath);
                }
                else
                {
                    throw new FileLoadException(String.Format("The configuration file {0} does not exist", fullPath));
                }
            }

        }

        private string GetFullPathToConfigFile()
        {
            // получаем адрес текущий репозиторий
            return new DirectoryInfo(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath)).FullName;
        }

      
    }
}
