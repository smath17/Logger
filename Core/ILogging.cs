namespace Logger.Core
{
    internal interface ILogging
    {
        public void Log(LogLevel level, string? message);
        public void Log(LogLevel level, Exception ex);
    }
}
