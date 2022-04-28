namespace webapi.Base
{
    /// <summary>
    /// AppDB Class
    /// </summary>
    public class AppDB : IDisposable
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public MySqlConnector.MySqlConnection Conn { get; }

        static AppDB()
        {
            var factory = Microsoft.Extensions.Logging.LoggerFactory.Create(
    builder => builder.AddFilter("MySqlConnector.SingleCommandPayloadCreator", LogLevel.Trace).AddConsole());
            MySqlConnector.Logging.MySqlConnectorLogManager.Provider = new MySqlConnector.Logging.MicrosoftExtensionsLoggingLoggerProvider(factory);
        }

        /// <summary>
        /// AppDB
        /// </summary>
        public AppDB(string connStr)
        {
            Conn = new MySqlConnector.MySqlConnection(connStr);
        }

        /// <summary>
        /// Release Connection
        /// </summary>
        public void Dispose()
        {
            Conn.Dispose();
        }
    }
}