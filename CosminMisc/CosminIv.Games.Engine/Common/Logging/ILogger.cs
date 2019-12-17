namespace CosminIv.Games.Engine.Common.Logging
{
    public interface ILogger
    {
        void Write(object value);
        void WriteLine(object value);
    }
}
