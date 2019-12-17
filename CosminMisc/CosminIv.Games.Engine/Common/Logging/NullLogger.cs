using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Common.Logging
{
    /// <summary>
    /// Logs nothing
    /// </summary>
    public class NullLogger : ILogger
    {
        public void Write(object value) {
        }

        public void WriteLine(object value) {
        }
    }
}
