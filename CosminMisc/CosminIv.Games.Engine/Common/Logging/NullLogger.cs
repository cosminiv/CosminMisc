namespace CosminIv.Games.Engine.Common.Logging
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
