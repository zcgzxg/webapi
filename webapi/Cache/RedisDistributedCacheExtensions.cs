using StackExchange.Redis;

namespace WebApi.Cache
{
    /// <summary>
    /// Redis分布式缓存扩展
    /// </summary>
    public static class RedisDistributedCacheExtensions
    {
        /// <summary>
        /// 添加Redis分布式缓存
        /// </summary>
        public static WebApplicationBuilder UseDistributedCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () =>
                {
                    var provider = builder.Services.BuildServiceProvider();
                    return Task.FromResult(provider.GetRequiredService<IConnectionMultiplexer>());
                };
                options.InstanceName = "webapi:";
            });
            return builder;
        }
    }
}