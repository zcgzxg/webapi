namespace webapi.Base
{
    public class AppDB : IDisposable
    {
        public MySqlConnector.MySqlConnection Conn { get; }

        static AppDB()
        {
            var factory = Microsoft.Extensions.Logging.LoggerFactory.Create(
    builder => builder.AddFilter("MySqlConnector.SingleCommandPayloadCreator", LogLevel.Trace).AddConsole());
            MySqlConnector.Logging.MySqlConnectorLogManager.Provider = new MySqlConnector.Logging.MicrosoftExtensionsLoggingLoggerProvider(factory);
        }

        public AppDB(string connStr)
        {
            Conn = new MySqlConnector.MySqlConnection(connStr);
        }

        public void Dispose()
        {
            Conn.Dispose();
        }
    }
}