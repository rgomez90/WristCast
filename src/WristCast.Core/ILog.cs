using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WristCast.Core
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
