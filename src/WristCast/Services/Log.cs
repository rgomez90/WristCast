using System;
using System.Runtime.CompilerServices;
using WristCast.Core.Services;

namespace WristCast.Services
{
    public class Log : ILog
    {
        private const string Logtag = "WristCast";

        public void Debug(string message, [CallerFilePath]string file = "", [CallerMemberName] string func = "", [CallerLineNumber]int line = 0)
        {
            global::Tizen.Log.Debug(Logtag, message, file, func, line);
        }
        public void Info(string message, [CallerFilePath]string file = "", [CallerMemberName] string func = "", [CallerLineNumber]int line = 0)
        {
            global::Tizen.Log.Info(Logtag, message, file, func, line);
        }
        public void Error(string message, Exception exception, [CallerFilePath]string file = "", [CallerMemberName] string func = "", [CallerLineNumber]int line = 0)
        {
            global::Tizen.Log.Error(Logtag, message + "\n" + exception.GetType() + "\n" + exception.Message, file, func, line);
        }
        public void Fatal(string message, Exception exception, [CallerFilePath]string file = "", [CallerMemberName] string func = "", [CallerLineNumber]int line = 0)
        {
            global::Tizen.Log.Fatal(Logtag, message + "\n" + exception.GetType() + "\n" + exception.Message, file, func, line);
        }
        public void Warn(string message, [CallerFilePath]string file = "", [CallerMemberName] string func = "", [CallerLineNumber]int line = 0)
        {
            global::Tizen.Log.Warn(Logtag, message, file, func, line);
        }
    }
}
