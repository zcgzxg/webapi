namespace webapi.Cache
{
    /// <summary>
    /// MemoryCache
    /// </summary>
    public class MemoryCache
    {
        /// <summary>
        /// MemoryCache
        /// <see cref="MemoryCache"/>
        /// </summary>
        public Microsoft.Extensions.Caching.Memory.IMemoryCache Cache;

        /// <summary>
        /// MemoryCache
        /// </summary>
        public MemoryCache()
        {
            Cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()
            {
                SizeLimit = 1024 * 1024,
            });
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
        public static void UseMemoryCache(this IServiceCollection services)
        {
            services.AddSingleton<MemoryCache>();
        }
    }
}