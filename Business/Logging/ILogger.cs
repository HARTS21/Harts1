using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Logging
{
    public interface ILogger
    {
        ILogger SetPriority(LogPriority logPriority);
        ILogger Token(string key, string value);
        ILogger Start();
        void End();
        LogPriority Priority { get; }
        List<ILogToken> Tokens { get; }
        void LogException(Exception exception);
    }
}
