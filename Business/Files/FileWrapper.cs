using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Business.Files
{
    public class FileWrapper : IFileWrapper
    {
        private readonly string _filePath;

        public FileWrapper(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string[] logger)
        {
            var logFileName = _filePath + "Tracelog" + DateTime.Now.Date.Day + DateTime.Now.Date.Month + DateTime.Now.Date.Year + ".txt";

            if (!File.Exists(logFileName))
            {
                File.WriteAllLines(logFileName, logger);
            }
            else
            {
                if (File.GetLastWriteTime(logFileName).ToShortDateString() != DateTime.Now.Date.ToShortDateString())
                {
                    File.WriteAllLines(logFileName, logger);
                }
                else
                {
                    using (var file = new StreamWriter(logFileName, true))
                    {
                        foreach (var t in logger)
                            file.WriteLine(t);
                    }
                }
            }
        }

        public string Read(string logfilePath)
        {
            var value = File.ReadAllText(logfilePath);
            return value;
        }
    }
}
