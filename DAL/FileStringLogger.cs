using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Infra;
using System.IO;

namespace DAL
{
    public class FileStringLogger : ILoggerStringService
    {
        // log to file in especific path
        public string LoggerPath { get; set; }

        public FileStringLogger(string paht = null)
        {
            if (paht == null)
            {
                string dataPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                Path.Combine(dataPath, "Log.txt");
            }
            else
                LoggerPath = paht;
        }

        public void Log(string logMessage)
        {
            if(!string.IsNullOrWhiteSpace(logMessage))
            {
                if (!File.Exists(LoggerPath))
                {
                    File.Create(LoggerPath);
                }

                File.AppendAllText(LoggerPath, logMessage);
            }
        }
    }
}
