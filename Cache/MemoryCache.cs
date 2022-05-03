using Microsoft.Extensions.Caching.Memory;

namespace webapi.Cache
{
    /// <summary>
    /// MemoryCache
    /// </summary>
    public class MemoryCache : IDisposable
    {
        /// <summary>
        /// MemoryCache
        /// <see cref="MemoryCache"/>
        /// </summary>
        public readonly IMemoryCache Cache;

        /// <summary>
        /// MemoryCache
        /// </summary>
        public MemoryCache(long sizeLimit)
        {
            Cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()
            {
                SizeLimit = sizeLimit,
            });
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Cache.Dispose();
        }
    }

    /// <summary>
    /// MemoryCacheExtensions
    /// </summary>
    public static class MemoryCacheExtensions
    {
        /// <summary>
        /// MemoryCache
        /// </summary>
        public static WebApplicationBuilder UseMemoryCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<MemoryCache>(new MemoryCache(
                long.Parse(builder.Configuration["Cache:MemoryCache:SizeLimit"])
            ));
            return builder;
        }
    }
}