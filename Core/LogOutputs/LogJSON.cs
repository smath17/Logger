using System.Text.Json;

namespace Logger.Core.LogOutputs
{
    internal class LogJSON(string path) : ILogging
    {
        readonly string path = path;

        public void WriteLog(Log log)
        {
            string json = JsonSerializer.Serialize(log);
            File.AppendAllText(path + @"\json\" + DateTime.Now.ToString("yyyy_MM_dd_HH") + ".log", json);
        }
    }
}
