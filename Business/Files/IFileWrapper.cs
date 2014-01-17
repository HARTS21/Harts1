using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Files
{
    interface IFileWrapper
    {
        void Write(string[] logger);
        string Read(string logfilePath);
    }
}
