using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Configuration;

using Business.Files;


namespace Business.Logging
{
    class Logger : ILogger
    {
        private LogPriority _logPriority;
        private readonly IFileWrapper _fileHandler;
        private readonly string _logDirPath;
        public Logger()
        {
            string logDir = System.Configuration.ConfigurationManager.AppSettings["LogDir"];
            _logDirPath = HttpContext.Current.Server.MapPath(logDir);
            _fileHandler = new FileWrapper(_logDirPath);
        }
        public string LogDirPath { get { return _logDirPath; } }
        #region Implementation of ILogger

        public ILogger SetPriority(LogPriority logPriority)
        {
            _logPriority = logPriority;
            return this;
        }

        public ILogger Token(string key, string value)
        {
            Tokens.Add(new LogToken(key, value));
            return this;
        }

        public ILogger Start()
        {
            Tokens = new List<ILogToken>();
            return this;
        }

        public void End()
        {
            _fileHandler.Write(new[] { GetString() });
            Tokens.Clear();
        }

        private string GetString()
        {
            var stringBuilder = new StringBuilder("Start : Time :");
            stringBuilder.Append(DateTime.UtcNow.ToString());
            stringBuilder.Append(GetPriorityString());
            foreach (var logToken in Tokens)
            {
                stringBuilder.Append(string.Format(" {0} {1} : {2} {3} ", "{", logToken.Key, logToken.Value, "}"));
            }
            stringBuilder.Append("End.");
            return stringBuilder.ToString();
        }



        private string GetPriorityString()
        {
            return string.Format(" {0} Priority : {1} {2}", "{", _logPriority, "}");
        }

        public LogPriority Priority { get; private set; }
        public List<ILogToken> Tokens { get; private set; }
        public void LogException(Exception exception)
        {
            Start().SetPriority(LogPriority.Error).Token("Exception Details", GetExceptionDetails(exception)).End();
        }

        #endregion

        private string GetExceptionDetails(Exception exception)
        {
            var result = exception.Message;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                result = exception.Message;
            }
            return result;
        }

    }
}
