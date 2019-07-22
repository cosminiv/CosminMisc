using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Common.Logging
{
    public interface ILogger
    {
        void Write(object value);
        void WriteLine(object value);
    }
}
