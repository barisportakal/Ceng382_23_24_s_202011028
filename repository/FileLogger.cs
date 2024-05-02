using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ReservationSystem
{
    public class FileLogger : ILogger  //single responsibilty örneği. ilogger sınıfını uygulamak log kayıtlarını tutmak için yazdım.
    {
        private readonly string logFilePath;
        private List<LogRecord> logList;

        public FileLogger(string logFilePath)
        {
            this.logFilePath = logFilePath;
            logList = new List<LogRecord>();
            LoadLogs();
        }

        private void LoadLogs()
        {
            if (File.Exists(logFilePath))
            {
                string json = File.ReadAllText(logFilePath);
                logList = JsonConvert.DeserializeObject<List<LogRecord>>(json); // List<LogRecord> olarak deserialize etmek
            }
            else
            {
                logList = new List<LogRecord>();
            }
        }

        public void Log(LogRecord log)
        {
            logList.Add(log);
            var json = JsonConvert.SerializeObject(logList);
            File.WriteAllText(logFilePath, json);
        }

        public IEnumerable<LogRecord> GetAllLogs()
        {
            return logList;
        }

        public IEnumerable<LogRecord> DisplayLogs(DateTime start, DateTime end)
        {
            List<LogRecord> filteredLogs = new List<LogRecord>();
            foreach (var record in logList)
            {
                if (record.Timestamp >= start && record.Timestamp <= end)
                {
                    filteredLogs.Add(record);
                }
            }
            return filteredLogs;
        }

        public IEnumerable<LogRecord> GetLogsWithReserverName(string reserverName)
        {
            return logList.FindAll(log => log.ReserverName.Equals(reserverName, StringComparison.OrdinalIgnoreCase));
        }
    }
}