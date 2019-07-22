using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Common
{
    public interface ILogger
    {
        void Write(object value);
        void WriteLine(object value);
    }
}
