using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CosminIv.Games.Common.Logging
{
    /// <summary>
    /// Logs to Debug
    /// </summary>
    public class DebugLogger : ILogger
    {
        public void Write(object value) {
            Debug.Write(value);
        }

        public void WriteLine(object value) {
            Debug.WriteLine(value);
        }
    }
}
