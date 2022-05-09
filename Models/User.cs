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

        /// <summary>
        /// 从另一个User复制数据
        /// </summary>
        public void CopyFrom(User? user)
        {
            if (user is not null)
            {
                ID = user.ID;
                Name = user.Name;
            }
        }
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