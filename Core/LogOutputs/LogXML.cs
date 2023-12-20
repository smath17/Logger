using System.Xml.Linq;

namespace Logger.Core.LogOutputs
{
    internal class LogXML(string path) : ILogging
    {
        public void WriteLogMsg(Log log)
        {
                XElement xmlEntry = new("logEntry",
                    new XElement("Level", log.Level),
                    new XElement("Date", DateTime.Now.ToString()),
                    new XElement("Message", log.Message));
                File.AppendAllText(path + @"\xml\" + DateTime.Now.ToString("yyyy_MM_dd_HH") + ".log", xmlEntry.ToString());
        }

        public void WriteLogEx(Log log)
        {
                XElement xmlEntry = new("logEntry",
                    new XElement("Level", log.Level),
                    new XElement("Date", DateTime.Now.ToString()),
                    new XElement("Exception",
                        new XElement("Source", log.Exception.Source),
                        new XElement("Message", log.Exception.Message),
                        new XElement("Stack", log.Exception.StackTrace)
                     ));

                // Add inner exception
                // TODO recursive through all inner exceptions
                if (log.Exception.InnerException != null)
                {
                    xmlEntry.Element("Exception").Add(
                        new XElement("InnerException",
                            new XElement("Source", log.Exception.InnerException.Source),
                            new XElement("Message", log.Exception.InnerException.Message),
                            new XElement("Stack", log.Exception.InnerException.StackTrace))
                        );
                }
            File.AppendAllText(path + @"\xml\" + DateTime.Now.ToString("yyyy_MM_dd_HH") + ".log", xmlEntry.ToString());
        }

        public void WriteLogAll(Log log)
        {
            XElement xmlEntry = new("logEntry",
                new XElement("Level", log.Level),
                new XElement("Date", DateTime.Now.ToString()),
                new XElement("Message", log.Message),
                new XElement("Exception",
                    new XElement("Source", log.Exception.Source),
                    new XElement("Message", log.Exception.Message),
                    new XElement("Stack", log.Exception.StackTrace)
                 ));

            // Add inner exception
            // TODO recursive through all inner exceptions
            if (log.Exception.InnerException != null)
            {
                xmlEntry.Element("Exception").Add(
                    new XElement("InnerException",
                        new XElement("Source", log.Exception.InnerException.Source),
                        new XElement("Message", log.Exception.InnerException.Message),
                        new XElement("Stack", log.Exception.InnerException.StackTrace))
                    );
            }
            File.AppendAllText(path + @"\xml\" + DateTime.Now.ToString("yyyy_MM_dd_HH") + ".log", xmlEntry.ToString());
        }

        public void WriteLog(Log log)
        {
            if (log.Exception != null && log.Message != null)
            {
                WriteLogAll(log);
            }
            else if (log.Message != null)
            {
                WriteLogMsg(log);
            }
            else if (log.Exception != null)
            {
                WriteLogEx(log);
            }
        }
    }
}
