namespace TAF
{

    using NLog;

    using TAF.Utility;

    public class LogManager : SingletonBase<LogManager>
    {
        private Logger _logger;

        public Logger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = NLog.LogManager.GetCurrentClassLogger();
                }
                return _logger;
            }
        }
    }
}