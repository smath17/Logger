using static Logger.Utility.ConfigReader;

namespace Logger.Core
{
    public class LogConfig
    {
        public readonly string baseDir;
        public readonly List<string> outputs;
        public readonly LogLevel minLogLevel;

        public LogConfig()
        {
            // TODO: Throw exception if missing app.config

            string logFilePathKey = "LogFilePath";
            // Obtain settings from app.config
            baseDir = ReadAppSetting(logFilePathKey, AppDomain.CurrentDomain.BaseDirectory + "\\Logs");

            outputs = ReadAppSettings("LogOutputs", new List<string> { "console" });

            Enum.TryParse(ReadAppSetting("MinLogLevel", "debug"), out minLogLevel);
        }
    }
}
