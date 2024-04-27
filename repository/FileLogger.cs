using System;
using System.IO;
using System.Text.Json;
using System.Globalization;
namespace ReservationSystem
{
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(LogRecord log)
        {
            var json = JsonSerializer.Serialize(log);
            File.AppendAllText(_logFilePath, json + Environment.NewLine);
        }
    }
}