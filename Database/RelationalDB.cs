using System.Data.Common;

namespace WebApi.Database
{
    /// <summary>
    /// AppDB Class
    /// </summary>
    class RelationalDB : IRelationalDB
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public DbConnection Conn { get; }

        /// <summary>
        /// AppDB
        /// </summary>
        public RelationalDB(string connStr)
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
    /// 注册IRelationalDB 
    /// </summary>
    public static class RelationalDBExtensions
    {
        /// <summary>
        /// 注册IRelationalDB
        /// </summary>
        public static WebApplicationBuilder UseRelationalDB(this WebApplicationBuilder builder, string connStr)
        {
            if (builder.Environment.IsDevelopment())
            {
                MySql.Data.MySqlClient.MySqlTrace.Switch.Level = System.Diagnostics.SourceLevels.All;
                MySql.Data.MySqlClient.MySqlTrace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
            }

            builder.Services.AddTransient<IRelationalDB>((sp) => new RelationalDB(connStr));
            return builder;
        }
    }
}