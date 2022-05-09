using Microsoft.AspNetCore.Authorization;

namespace webapi.Authorization
{
    /// <summary>
    /// 权限验证扩展
    /// </summary>
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// 使用权限验证
        /// </summary>
        public static WebApplicationBuilder UseAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });
            return builder;
        }
    }
}