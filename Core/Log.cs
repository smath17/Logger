namespace Logger.Core
{
    public class Log
    {
        public Log(Exception? exception, LogLevel level)
        {
            Exception = exception;
            Level = level;
        }

        public Log(string? message, LogLevel level)
        {
            Message = message;
            Level = level;
        }

        public Log(Exception? exception, string? message, LogLevel level)
        {
            Exception = exception;
            Message = message;
            Level = level;
        }

        private Exception? _exception;
        private string? _message;
        private LogLevel _level;

        public Exception? Exception { get => _exception; set => _exception = value; }
        public string? Message { get => _message; set => _message = value; }
        public LogLevel Level { get => _level; set => _level = value; }
    }
}
