using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Logging
{
    public interface ILogToken
    {
        string Key { get; }
        string Value { get; }
    }
}
