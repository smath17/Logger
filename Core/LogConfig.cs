using System.Configuration;

namespace Logger.Core
{
    public class LogConfig
    {
        string baseDir;

        public LogConfig()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains("myKey"))
            {
                baseDir = ConfigurationManager.AppSettings["LogFilePath"];
            }
            else
            {
                // Key doesn't exist
            }

        }
    }
}
