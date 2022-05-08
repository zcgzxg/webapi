namespace webapi.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public uint ID { get; set; } = 0;
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }


    /// <summary>
    /// 用户信息注入
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// 用户信息注入
        /// </summary>
        public static WebApplicationBuilder UseUser(this WebApplicationBuilder app)
        {
            app.Services.AddScoped<User>();
            return app;
        }
    }
}