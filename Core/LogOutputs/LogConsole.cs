namespace Logger.Core.LogOutputs
{
    internal class LogConsole : ILogging
    {

        public void WriteLog(Log log)
        {
            if (log.Exception != null && log.Message != null)
            {
                Console.WriteLine(log.Level.ToString() + ": " + log.Message.ToString() + " " + log.Exception.ToString() + "Date: " + DateTime.Now.ToString());
            }
            else if (log.Exception != null)
            {
                Console.WriteLine(log.Level.ToString() + ": " + log.Exception.ToString() + "Date: " + DateTime.Now.ToString());
            }
            else if (log.Message != null)
            {
                Console.WriteLine(log.Level.ToString() + ": " + log.Message + "Date: " + DateTime.Now.ToString());

            }
            else
            {
                Console.WriteLine(log.Level.ToString() + " " + "Date: " + DateTime.Now.ToString());
            }
        }
    }
}
