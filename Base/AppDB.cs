using System.Data.Common;

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
        public DbConnection Conn { get; }

        /// <summary>
        /// AppDB
        /// </summary>
        public AppDB(string connStr)
        {
            Conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
        }

        /// <summary>
        /// Release Connection
        /// </summary>
        public void Dispose()
        {
            Conn.Dispose();
        }
    }

    /// <summary>
    /// 注册AppDB 
    /// </summary>
    public static class AppDBExtensions
    {
        /// <summary>
        /// 注册AppDB
        /// </summary>
        public static WebApplicationBuilder UseAppDB(this WebApplicationBuilder builder, string connStr)
        {
            if (builder.Environment.IsDevelopment())
            {
                MySql.Data.MySqlClient.MySqlTrace.Switch.Level = System.Diagnostics.SourceLevels.All;
                MySql.Data.MySqlClient.MySqlTrace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
            }

            builder.Services.AddTransient<AppDB>((sp) => new AppDB(connStr));
            return builder;
        }
    }
}