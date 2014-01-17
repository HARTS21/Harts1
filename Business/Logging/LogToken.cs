using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Logging
{
    class LogToken : ILogToken
    {
        public LogToken(string key, string value)
        {
            Key = key;
            Value = value;
        }

        #region Implementation of ILogToken

        public string Key { get; private set; }
        public string Value { get; private set; }

        #endregion
    }
}
