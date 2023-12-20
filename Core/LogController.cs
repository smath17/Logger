using Logger.Core.LogOutputs;
using System.Collections.Concurrent;

namespace Logger.Core
{
    public static class LogController
    {
        private static readonly LogConfig _config = new();
        private static readonly LogConsole _logConsole = new();
        private static readonly LogXML _logXML = new(_config.baseDir);
        private static readonly LogJSON _logJSON = new(_config.baseDir);
        private static readonly BlockingCollection<Log> _logQueue = new();
        private static Task? _backgroundLogWriter;
        private static bool StopQueueingNewLogs;
        private static bool StopLoggingRequested;
        private static readonly object _syncLock = new();


        public static void Log(LogLevel level, string message, Exception ex)
        {
            if (_backgroundLogWriter == null)
            {
                StartLogging();
            }
            Log log = new(ex, message, level);
            _logQueue.Add(log);
            
        }

        public static void Log(LogLevel level, string message)
        {
            if (_backgroundLogWriter == null)
            {
                StartLogging();
            }
            Log log = new(message, level);
            _logQueue.Add(log);

        }

        public static void Log(LogLevel level, Exception? ex)
        {
            if (_backgroundLogWriter == null)
            {
                StartLogging();
            }
            Log log = new(ex, level);
            _logQueue.Add(log);

        }

        public static void Log(LogLevel level)
        {
            if (_backgroundLogWriter == null)
            {
                StartLogging();
            }
            Log log = new(level);
            _logQueue.Add(log);

        }

        public static void StartLogging()
        {
            // Return if task already running
            if (_backgroundLogWriter != null || StopQueueingNewLogs || StopLoggingRequested)
            {
                return;
            }
            // Reset flags
            StopQueueingNewLogs = false;
            StopLoggingRequested = false;

            // Create task and start
            _backgroundLogWriter = new Task(WriteLogToOutput, TaskCreationOptions.LongRunning);
            _backgroundLogWriter.Start();
        }

        public static void StopLogging()
        {
            StopQueueingNewLogs = true;

            FlushQueue();
            StopLoggingRequested = true;
            _backgroundLogWriter = null;
        }

        private static void WriteLogToOutput()
        {
            // Aqcuire lock to avoid multiple writes to same file
            // Not really, LogOutputs are callable on their own
            if (Monitor.TryEnter(_syncLock))
            {
                try
                {
                    while (!StopLoggingRequested)
                    {
                        foreach (var log in _logQueue.GetConsumingEnumerable())
                        {

                            if (log.Level > _config.minLogLevel)
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
                finally
                {
                    // Relinquish control of lock
                    Monitor.Exit(_syncLock); 
                }
            }
        }

        private static void FlushQueue()
        {
            while (_logQueue.Count > 0)
            {
                // HACK: Wait while background task catches up
                Thread.Sleep(200);
            }
        }
    }
}
