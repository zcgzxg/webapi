using Microsoft.AspNetCore.Authorization;

using WebApi.Models;

namespace WebApi.Authorization
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
            builder.Services.AddScoped<IAuthorizationHandler, AtLeastUserIDHandler>();
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("AtLeastUserId10", policy => policy.RequireClaim(JWTTokenConfig.ClaimType).AddRequirements(new AtLeastUserIDRequirement(10)));
            });
            return builder;
        }
    }
}