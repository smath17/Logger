using Logger.Core.LogOutputs;

namespace Logger.Core
{
    public class LogController
    {
        private static readonly LogConfig _config = new();
        private static readonly LogConsole _logConsole = new();
        private static readonly LogXML _logXML = new(_config.baseDir);
        private static readonly LogJSON _logJSON = new(_config.baseDir);

        public static void Log(LogLevel level, string? message, Exception? ex)
        {
            Log log = new(ex, message, level);
            if (level > _config.minLogLevel)
            {
                if (_config.outputs.Contains("console"))
                {
                    _logConsole.WriteLog(log);
                }
                if (_config.outputs.Contains("xml"))
                {
                    _logXML.WriteLog(log);
                }
                if (_config.outputs.Contains("json"))
                {
                    _logJSON.WriteLog(log);
                }
            }
        }
    }
}
