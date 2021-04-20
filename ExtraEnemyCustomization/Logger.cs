using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraEnemyCustomization
{
    public static class Logger
    {
        public static ManualLogSource LogInstance;
        public static bool UsingDevMessage = true;

        public static void Log(string format, params object[] args) => Log(string.Format(format, args));
        public static void Log(string str)
        {
            LogInstance?.Log(LogLevel.Message, str);
        }

        public static void Warning(string format, params object[] args) => Warning(string.Format(format, args));
        public static void Warning(string str)
        {
            LogInstance?.Log(LogLevel.Warning, str);
        }

        public static void Error(string format, params object[] args) => Error(string.Format(format, args));
        public static void Error(string str)
        {
            LogInstance?.Log(LogLevel.Error, str);
        }

        public static void DevMessage(string format, params object[] args) => DevMessage(string.Format(format, args));
        public static void DevMessage(string str)
        {
            if (UsingDevMessage)
                LogInstance?.LogDebug(str);
        }
    }
}
