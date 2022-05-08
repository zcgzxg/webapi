using StackExchange.Redis;

namespace webapi.Database
{
    /// <summary>
    /// Redis Extensions
    /// </summary>
    public static class RedisExtensions
    {
        /// <summary>
        /// Adds a ConnectionMultiplexer and Redis IDatabase to the services container.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="connectionString">The connection string.</param>
        public static void AddRedis(this IServiceCollection services, string connectionString)
        {
            var multiplexer = ConnectionMultiplexer.Connect(connectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddScoped<IDatabase>((sp) => multiplexer.GetDatabase());
        }
    }
}
