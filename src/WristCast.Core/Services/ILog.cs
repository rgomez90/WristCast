using System;

namespace WristCast.Core.Services
{
    public interface ILog
    {
        void Debug(string message, string file = "", string func = "", int line = 0);
        void Error(string message, Exception exception = null, string file = "", string func = "",int line = 0);
        void Info(string message, string file = "", string func = "", int line = 0);
        void Warn(string message, string file = "", string func = "", int line = 0);
        void Fatal(string message, Exception exception = null, string file = "", string func = "", int line = 0);
    }


}
