using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp1
{
    public class PerformanceMonitor
    {
        private Stack<MethodCall> _callStack = new Stack<MethodCall>();
        private Dictionary<string, MethodCallStatistic> _callsStats = new Dictionary<string, MethodCallStatistic>();
        private Stopwatch _stopwatch = new Stopwatch();

        private readonly bool _isEnabled = Settings.IsEnabled;
        private readonly string _logFile = Settings.LogFile;
        private readonly int _minDurationToLogMs = Settings.MinimumDurationToLogMs;
        private readonly int _maxFileSize = 1 << 20;

        /// <summary>
        /// Can call it like this: perfMon.StartCall(GetCaller());
        /// </summary>
        public void StartCall(string methodName)
        {
            try
            {
                if (!_isEnabled)
                    return;

                if (_callStack.Count == 0)
                    _stopwatch.Start();

                _callStack.Push(new MethodCall { MethodName = methodName, StartTimeMillisec = _stopwatch.ElapsedMilliseconds });
            }
            catch (Exception ex)
            {
                Debug.Assert(false);
                Debug.Print("ERROR PerformanceMonitor: " + ex.ToString());
            }
        }

        // Get name of current method
        //public static string GetCaller([CallerMemberName] string caller = null)
        //{
        //    return caller;
        //}

        public void EndCall()
        {
            try
            {
                if (!_isEnabled)
                    return;

                if (_callStack.Count == 0)
                    Write("ERROR PerformanceMonitor: Mismatched Start/End calls\n");

                MethodCall call = _callStack.Pop();
                long callTime = _stopwatch.ElapsedMilliseconds - call.StartTimeMillisec;
                call.TotalTimeMillisec += callTime;

                foreach (MethodCall callInStack in _callStack)
                {
                    callInStack.TotalTimeMillisec -= call.TotalTimeMillisec;
                }

                AddStatistic(call);

                if (_callStack.Count == 0)
                    DisplayStats();
            }
            catch (Exception ex)
            {
                Debug.Assert(false);
                Debug.Print("ERROR PerformanceMonitor: " + ex.ToString());
            }
        }

        public void Write(string text)
        {
            try
            {
                if (_isEnabled && !string.IsNullOrWhiteSpace(_logFile))
                {
                    DeleteFileIfTooBig();
                    File.AppendAllText(_logFile, text);
                }
            }
            catch (Exception ex)
            {
                //Debug.Assert(false);
                Debug.Print("ERROR PerformanceMonitor: " + ex.ToString());
            }
        }

        private void DeleteFileIfTooBig()
        {
            if (File.Exists(_logFile))
            {
                FileInfo fi = new FileInfo(_logFile);
                if (fi.Length > _maxFileSize)
                    File.Delete(_logFile);
            }
        }

        private void DisplayStats()
        {
            List<MethodCallStatistic> statsList = _callsStats.Select(p => p.Value).OrderByDescending(s => s.TotalTimeMillisec).ToList();
            StringBuilder sb = new StringBuilder();

            foreach (MethodCallStatistic stat in statsList)
            {
                if (stat.TotalTimeMillisec >= _minDurationToLogMs)
                {
                    sb.AppendLine($"{DateTime.Now.ToString("G")} {stat.MethodName}: {stat.TotalTimeMillisec}ms ({stat.CallCount})");
                }
            }

            Write(sb.ToString());
        }

        private void AddStatistic(MethodCall call)
        {
            bool found = _callsStats.TryGetValue(call.MethodName, out MethodCallStatistic stat);

            if (found == false)
            {
                stat = new MethodCallStatistic
                {
                    MethodName = call.MethodName
                };
                _callsStats.Add(call.MethodName, stat);
            }

            stat.CallCount++;
            stat.TotalTimeMillisec += call.TotalTimeMillisec;
        }

        class Settings
        {
            public static bool IsEnabled
            {
                get
                {
                    string isEnabledStr = ConfigurationManager.AppSettings["PerformanceMonitor.Enabled"];
                    bool.TryParse(isEnabledStr, out bool result);
                    return result;
                }
            }

            public static string LogFile
            {
                get
                {
                    string file = ConfigurationManager.AppSettings["PerformanceMonitor.LogFile"] ?? "";

                    if (file.StartsWith("~") && HttpContext.Current != null)
                        file = HttpContext.Current.Server.MapPath(file);

                    return file;
                }
            }

            public static int MinimumDurationToLogMs
            {
                get
                {
                    string minDurationStr = ConfigurationManager.AppSettings["PerformanceMonitor.MinimumDurationToLogMs"];
                    int.TryParse(minDurationStr, out int n);
                    return n;
                }
            }
        }

        class MethodCall
        {
            public string MethodName { get; set; }
            public long StartTimeMillisec { get; set; }
            public long TotalTimeMillisec { get; set; }
        }

        class MethodCallStatistic
        {
            public string MethodName { get; set; }
            public long TotalTimeMillisec { get; set; }
            public int CallCount { get; set; }
        }
    }
}
