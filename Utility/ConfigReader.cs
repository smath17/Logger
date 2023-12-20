using System.ComponentModel;
using System.Configuration;

namespace Logger.Utility
{
    internal static class ConfigReader
    {
        // Try to convert config value or return default
        internal static T ReadAppSetting<T>(string searchKey, T defaultValue)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(searchKey))
            {
                try
                { // see if it can be converted. Skip if string
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null && typeof(T) != typeof(String))
                    {
                        defaultValue = (T)converter.ConvertFromString(ConfigurationManager.AppSettings.GetValues(searchKey).First());
                    }
                }
                catch { } // nothing to do just return the defaultValue
            }
            return defaultValue;
        }

        // Try to convert config semi-colon seperated list or return default
        internal static List<T> ReadAppSettings<T>(string searchKey, List<T> defaultValues)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(searchKey))
            {
                try
                {
                    List<string> keyValues = new(ConfigurationManager.AppSettings[searchKey].Split([';']));

                    // Convert each member of list
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        var convertedList = new List<T>();
                        foreach (var value in keyValues)
                        {
                            convertedList.Add((T)converter.ConvertFromString(value));
                        }
                        return convertedList;
                    }
                }
                catch { } // nothing to do just return the defaultValue
            }

            return defaultValues;
        }
    }
}
