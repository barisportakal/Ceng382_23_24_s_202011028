namespace ReservationSystem
{

    public interface ILogger
    {
        void Log(LogRecord log);
        IEnumerable<LogRecord> GetAllLogs();
        IEnumerable<LogRecord> GetLogsWithReserverName(string reserverName);
        IEnumerable<LogRecord> DisplayLogs(DateTime start, DateTime end);
    }
}