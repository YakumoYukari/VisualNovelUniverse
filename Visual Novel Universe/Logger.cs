using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Visual_Novel_Universe
{
    public interface ILogger
    {
        void Log(string Msg);
        void LogWarning(string Msg);
        void LogError(string Msg);
    }

    public class Logger : ILogger
    {
        public static readonly Logger Instance = new Logger();
        private readonly string _LogFile;

        private const int LogFileMaxLines = 3000;

        private Logger()
        {
            _LogFile = ConfigurationManager.AppSettings["LogFilepath"];

            VerifyLogFile();
        }

        private void VerifyLogFile()
        {
            if (!File.Exists(_LogFile))
                File.WriteAllText(_LogFile, "");
        }

        public void Log(string Msg)
        {
            DoLog(AppendTimestamp(Msg));
        }

        public void LogWarning(string Msg)
        {
            DoLog($"[WARNING]: {AppendTimestamp(Msg)}");
        }

        public void LogError(string Msg)
        {
            DoLog($"[ERROR]: {AppendTimestamp(Msg)}");
        }

        private static string AppendTimestamp(string Msg)
        {
            var TimeStamp = DateTime.Now;
            return TimeStamp.ToShortDateString() + " " + TimeStamp.ToLongTimeString() + ": " + Msg;
        }

        private void DoLog(string Msg)
        {
            CycleLogs();
            VerifyLogFile();
            File.AppendAllText(_LogFile, Msg + Environment.NewLine);
            EchoToConsole(Msg);
        }

        private static void EchoToConsole(string Msg)
        {
            Console.WriteLine(Msg);
        }

        private void CycleLogs()
        {
            try
            {
                var Lines = File.ReadAllLines(_LogFile);
                if (Lines.Length <= LogFileMaxLines) return;

                VerifyLogFile();
                var RecentHalf = Lines.Skip(LogFileMaxLines/2);
                File.WriteAllLines(_LogFile, RecentHalf.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
